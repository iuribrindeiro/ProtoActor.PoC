using Proto;
using Proto.Cluster;
using Proto.Persistence;
using Proto.Persistence.SnapshotStrategies;
using ProtoCluster;
using static IEventGrainEvents;

public class EventGrain : EventGrainBase
{
    private readonly string _id;
    private readonly ILogger<EventGrain> _logger;
    private readonly Persistence _persistence;
    private Event _event;

    public EventGrain(IProvider provider, IContext context,
        ClusterIdentity clusterIdentity, ILogger<EventGrain> logger) : base(context)
    {
        _persistence = Persistence.WithEventSourcingAndSnapshotting(
            provider,
            provider,
            clusterIdentity.Identity,
            ApplyEvent, ApplySnapshot, new IntervalStrategy(1000),
            () => _event);
        _id = clusterIdentity.Identity;
        _logger = logger;
        _event = new Event(_id, string.Empty);
    }

    public override async Task<AddTicketsResponse> AddTickets(AddTicketsRequest request)
    {
        var isNewTicket = !_event.Tickets.Select(a => a.Id).Contains(request.Ticket.Id);

        if (isNewTicket)
        {
            _logger.LogInformation("Adding tickets to event {EventId}", _id);
            await _persistence.PersistEventAsync(new TicketsAdded(request.Ticket));
            _logger.LogInformation("New ticket added to event {EventId}, with a total of {Total} tickets now",
                _event.Id, _event.Tickets.Count);
            return new AddTicketsResponse { TicketAdded = true };
        }

        _logger.LogWarning("Ticket already added to event {EventId}", _id);
        return new AddTicketsResponse { TicketAdded = false };
    }

    public override async Task CreateEvent(CreateEventRequest request)
    {
        await _persistence.PersistEventAsync(new EventCreated(request.Name));
    }

    public override Task<GetTotalTicketsResponse> GetTotalTickets()
    {
        return Task.FromResult(new GetTotalTicketsResponse() { Total = _event.Tickets.Count });
    }

    public override Task<GetTotalTicketsResponse> GetTotalTicketsScanned()
    {
        return Task.FromResult(new GetTotalTicketsResponse()
        {
            Total = _event.Tickets.Count(a => a.Scanned)
        });
    }

    public override async Task<ScanTicketResponse> ScanTicket(ScanTicketRequest request)
    {
        var ticketToScan = _event.Tickets.FirstOrDefault(a => a.Id == request.TicketId);

        if (ticketToScan is { Id: "" or null })
        {
            _logger.LogWarning("Ticket {TicketId} not found in event {EventId}", request.TicketId, _id);
            return new ScanTicketResponse { TicketScanned = false };
        }

        if (ticketToScan.Scanned)
        {
            _logger.LogWarning("Ticket {TicketId} already scanned in event {EventId}", request.TicketId, _id);
            return new ScanTicketResponse { TicketScanned = false };
        }

        _logger.LogInformation("Scanning ticket {TicketId} in event {EventId}", request.TicketId, _id);

        await _persistence.PersistEventAsync(new TicketScanned(ticketToScan.Id));

        _logger.LogInformation("Ticket {TicketId} scanned in event {EventId}", request.TicketId, _id);

        return new ScanTicketResponse { TicketScanned = true };
    }

    public override Task<GetTicketsResponse> GetTickets()
    {
        return Task.FromResult(new GetTicketsResponse()
        {
            Tickets =
            {
                _event.Tickets.Select(e => new GetTicketsResponse.Types.Ticket()
                    { Gate = e.Gate, Id = e.Id, Scanned = e.Scanned, Seat = e.Seat })
            }
        });
    }

    public override async Task OnStarted()
    {
        await _persistence.RecoverStateAsync();
        Context.SetReceiveTimeout(TimeSpan.FromMinutes(1));
    }

    private void ApplySnapshot(Snapshot snapshot)
    {
        if (snapshot.State is Event evt) _event = evt;
    }

    private void ApplyEvent(Proto.Persistence.Event @event)
    {
        _event = @event.Data switch
        {
            EventCreated created => new Event(_id, created.Name, []),
            TicketsAdded added => AddTicket(added),
            TicketScanned scanned => ScanTicket(scanned),
            _ => _event
        };
    }

    private Event ScanTicket(TicketScanned scanned)
    {
        _event.Tickets.ReplaceOne(a => a.Id == scanned.TicketId, a => a with { Scanned = true });
        return _event;
    }

    private Event AddTicket(TicketsAdded added)
    {
        _event.Tickets.Add(ToTicket(added.NewTicket));
        return _event;
    }

    public override Task OnStopped()
    {
        _logger.LogInformation("Event {EventId} stopped", _id);
        return Task.CompletedTask;
    }

    public override Task OnReceive()
    {
        _logger.LogInformation("Event {EventId} received message {Message}", _id, Context.Message);
        switch (Context.Message)
        {
            case ReceiveTimeout _:
                _logger.LogInformation("Event {EventId} receive timeout", _id);
                Context.Stop(Context.Self);
                _logger.LogInformation("Event {EventId} asked to stop", _id);
                break;
        }

        return Task.CompletedTask;
    }

    private Ticket ToTicket(NewTicket newTicket)
    {
        return new Ticket(newTicket.Id, newTicket.Seat, newTicket.Gate, _id, false);
    }
}