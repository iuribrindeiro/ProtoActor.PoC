using Proto;
using Proto.Cluster;
using Proto.Persistence;
using Proto.Persistence.SnapshotStrategies;
using ProtoClusterTutorial;

public record struct EventCreated(string Name);

public record struct TicketsAdded(AddTicketsRequest.Types.NewTicket[] NewTickets);

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
            ApplyEvent, ApplySnapshot, new IntervalStrategy(50),
            getSnapshot: () => _event);
        _id = clusterIdentity.Identity;
        _logger = logger;
    }

    public override async Task AddTickets(AddTicketsRequest request)
    {
        _logger.LogInformation("Adding tickets to event {EventId}", _id);
        await _persistence.PersistEventAsync(new TicketsAdded(request.Tickets.ToArray()));
    }

    public override async Task CreateEvent(CreateEventRequest request) =>
        await _persistence.PersistEventAsync(new EventCreated(request.Name));

    public override async Task OnStarted()
    {
        await _persistence.RecoverStateAsync();
        Context.SetReceiveTimeout(TimeSpan.FromMinutes(1));
    }

    private void ApplySnapshot(Snapshot snapshot)
    {
        if (snapshot.State is Event @event) _event = @event;
    }

    private void ApplyEvent(Proto.Persistence.Event @event)
    {
        var newEvent = @event.Data switch
        {
            EventCreated created => new Event(_id, created.Name, []),
            TicketsAdded added => _event with
            {
                Id = _id,
                Tickets = (_event.Tickets ?? []).Concat(added.NewTickets.Select(ToTicket)).ToArray()
            },
            _ => _event
        };

        _event = newEvent;
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

    private Ticket ToTicket(AddTicketsRequest.Types.NewTicket newTicket)
        => new(newTicket.Id, newTicket.Seat, newTicket.Gate, _id);
}