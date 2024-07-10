using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Proto.Cluster;
using Proto.Cluster.Consul;
using Proto.Cluster.PartitionActivator;
using Proto.OpenTelemetry;
using Proto.Remote;
using ProtoClusterTutorial;

var builder = WebApplication.CreateBuilder(args);

var tracingOtlpEndpoint = "http://localhost:4317";

builder.Services
    .AddOpenTelemetry()
    .ConfigureResource(e => e.AddService("ProtoActor.PoC"))
    .WithMetrics(metrics => metrics
        .AddAspNetCoreInstrumentation()
        .AddProtoActorInstrumentation()
        .AddOtlpExporter(pro => { pro.Endpoint = new Uri(tracingOtlpEndpoint); }))
    .WithTracing(tracing =>
    {
        tracing
            .AddAspNetCoreInstrumentation()
            .AddProtoActorInstrumentation()
            .AddOtlpExporter(otlpOptions => otlpOptions.Endpoint = new Uri(tracingOtlpEndpoint));
    });


builder.Services.AddProtoCluster("EventsCluster",
    configureRemote: r => r.WithProtoMessages(GrainsReflection.Descriptor),
    configureSystem: e => e.WithMetrics(),
    configureCluster: c => c,
    clusterProvider: new ConsulProvider(new ConsulProviderConfig(),
        clientConfiguration: c => c.Address = new Uri("http://localhost:8500")),
    identityLookup: new PartitionActivatorLookup(),
    runAsClient: true);

var app = builder.Build();

app.MapPost("/add-tickets",
    handler: async (AddTicketsInput input, Cluster cluster, CancellationToken cToken)
        =>
    {
        await cluster
            .GetEventGrain(input.EventId)
            .AddTickets(new AddTicketsRequest
            {
                EventId = input.EventId,
                Tickets =
                {
                    input.Tickets.Select(t => new AddTicketsRequest.Types.NewTicket
                    {
                        Id = t.Id,
                        Seat = t.Seat,
                        Gate = t.Gate
                    })
                }
            }, cToken);
        return Results.NoContent();
    });

app.Run();