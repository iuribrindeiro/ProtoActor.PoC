using System.Text.Json.Serialization;
using Messages;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Proto.Cluster;
using Proto.Cluster.Consul;
using Proto.Cluster.PartitionActivator;
using Proto.OpenTelemetry;
using Proto.Remote;
using ProtoCluster;
using ActorMessages = ProtoCluster.GrainsReflection;

var builder = WebApplication.CreateSlimBuilder(args);

var tracingOtlpEndpoint = "http://localhost:4317";

var serviceName = "WebApi";

builder.Logging.AddOpenTelemetry(e =>
{
    e.IncludeFormattedMessage = true;
    e.AddOtlpExporter();
});

builder.Services
    .AddResourceMonitoring()
    .AddHealthChecks();

builder.Services
    .AddOpenTelemetry()
    .ConfigureResource(e => e.AddService(serviceName))
    .WithMetrics(metrics => metrics
        .AddAspNetCoreInstrumentation()
        .AddProtoActorInstrumentation()
        .AddProcessInstrumentation()
        .AddRuntimeInstrumentation()
        .AddOtlpExporter(pro => { pro.Endpoint = new Uri(tracingOtlpEndpoint); }))
    .WithTracing(tracing =>
    {
        tracing
            .AddAspNetCoreInstrumentation()
            .AddProtoActorInstrumentation()
            .AddSource(serviceName)
            .AddOtlpExporter(otlpOptions => otlpOptions.Endpoint = new Uri(tracingOtlpEndpoint));
    });

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

builder.Services.AddProtoCluster("EventsCluster",
    configureRemote: e => e.WithProtoMessages(ActorMessages.Descriptor),
    configureSystem: e => e.WithMetrics(),
    configureCluster: c => c,
    clusterProvider: new ConsulProvider(new ConsulProviderConfig(),
        c => c.Address = new Uri("http://localhost:8500")),
    identityLookup: new PartitionActivatorLookup(),
    runAsClient: true);

var app = builder.Build();

app.MapHealthChecks("health");

app.MapPost("/add-ticket/{eventId}",
    async (string eventId, AddTicketInput input, Cluster cluster, CancellationToken cToken)
        =>
    {
        var result = await cluster
            .GetEventGrain(eventId)
            .AddTickets(new AddTicketsRequest
            {
                Ticket = new NewTicket
                {
                    Id = input.Id,
                    Seat = input.Seat,
                    Gate = input.Gate
                }
            }, cToken);

        return result switch
        {
            { TicketAdded: true } => Results.Ok(),
            { TicketAdded: false } => Results.NoContent(),
            _ => Results.StatusCode(500)
        };
    });

app.MapPost("/scan-ticket/{eventId}/{ticketId}",
    async (string eventId, string ticketId, Cluster cluster, CancellationToken cToken) =>
    {
        var result = await cluster
            .GetEventGrain(eventId)
            .ScanTicket(new ScanTicketRequest
            {
                TicketId = ticketId
            }, cToken);

        return result switch
        {
            { TicketScanned: true } => Results.Ok(),
            { TicketScanned: false } => Results.NoContent(),
            _ => Results.StatusCode(500)
        };
    });

app.MapGet("/tickets/{eventId}", (string eventId, Cluster cluster, CancellationToken cancellationToken)
    => cluster.GetEventGrain(eventId).GetTickets(cancellationToken));

app.MapGet("/total-tickets/{evtId}", (string evtId, Cluster cluster, CancellationToken cancellationToken)
    => cluster.GetEventGrain(evtId).GetTotalTickets(cancellationToken));

app.MapGet("/total-tickets-scanned/{evtId}", (string evtId, Cluster cluster, CancellationToken cancellationToken) =>
    cluster.GetEventGrain(evtId).GetTotalTicketsScanned(cancellationToken));

app.Run();


[JsonSerializable(typeof(AddTicketInput[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}