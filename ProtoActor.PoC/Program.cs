using System.Text.Json;
using System.Text.Json.Serialization;
using JasperFx.CodeGeneration;
using Marten;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Proto;
using Proto.Cluster;
using Proto.Diagnostics;
using Proto.OpenTelemetry;
using Proto.Remote.HealthChecks;
using ProtoCluster;
using Weasel.Core;
using static IEventGrainEvents;

var builder = WebApplication.CreateSlimBuilder(args);

var tracingOtlpEndpoint = "http://localhost:4317";

var serviceName = "ProtoActor.PoC";

builder.Logging.AddOpenTelemetry(e =>
{
    e.IncludeFormattedMessage = true;
    e.AddOtlpExporter();
});

builder.Services.AddMarten(options =>
{
    // Establish the connection string to your Marten database
    options.Connection(builder.Configuration.GetConnectionString("Default")!);

    // Specify that we want to use STJ as our serializer
    options.UseSystemTextJsonForSerialization(configure: e =>
        e.TypeInfoResolverChain.Add(AppJsonSerializerContext.Default));

    options.Schema.For<ProtoActor.PoC.Persistence.Marten.Snapshot<Event>>()
        .Index(e => e.ActorName);

    options.Schema.For<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>>()
        .Index(e => e.ActorName);

    options.GeneratedCodeMode = TypeLoadMode.Static;
    options.AutoCreateSchemaObjects = AutoCreate.CreateOnly;
});

// Configure OpenTelemetry Resources with the application name
builder.Services
    .AddOpenTelemetry()
    .ConfigureResource(e => e
        .AddService(
            serviceName,
            serviceVersion: System.Reflection.Assembly.GetEntryAssembly()?.GetName().Version?.ToString(3))
        .AddAttributes(new Dictionary<string, object>
        {
            { "host.name", Environment.GetEnvironmentVariable("HOST_NAME") ?? Environment.MachineName }
        }))
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


builder.Services.AddActorSystem(builder.Configuration);
builder.Services.AddHostedService<ActorSystemClusterHostedService>();
builder.Services.ConfigureHttpJsonOptions(e =>
{
    e.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    e.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

builder.Services
    .AddResourceMonitoring()
    .AddHealthChecks()
    .AddCheck<ActorSystemHealthCheck>("actor-system");


var app = builder.Build();

Log.SetLoggerFactory(app.Services.GetRequiredService<ILoggerFactory>());

app.MapGet("/diagnostics", (ActorSystem system) => system.Diagnostics.GetDiagnostics());

app.MapHealthChecks("/health");

app.Run();

public class ActorSystemClusterHostedService(Cluster cluster, ILogger<ActorSystemClusterHostedService> logger)
    : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting a cluster member");

        await cluster
            .StartMemberAsync();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Shutting down a cluster member");

        await cluster
            .ShutdownAsync();
    }
}

public record struct CreateEventInput(string Id, string Name);

public record struct Ticket(string Id, string Seat, string Gate, string EvnetId, bool Scanned);

public record struct Event(string Id, string Name, List<Ticket>? Tickets = null)
{
    public List<Ticket> Tickets { get; init; } = Tickets ?? [];
}

[JsonSerializable(typeof(DiagnosticsEntry))]
[JsonSerializable(typeof(Ticket))]
[JsonSerializable(typeof(EventCreated))]
[JsonSerializable(typeof(TicketsAdded))]
[JsonSerializable(typeof(IEventGrainEvents))]
[JsonSerializable(typeof(NewTicket))]
[JsonSerializable(typeof(Event))]
[JsonSerializable(typeof(ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>))]
[JsonSerializable(typeof(ProtoActor.PoC.Persistence.Marten.Snapshot<Event>))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}

[JsonDerivedType(typeof(EventCreated), nameof(EventCreated))]
[JsonDerivedType(typeof(TicketsAdded), nameof(TicketsAdded))]
[JsonDerivedType(typeof(TicketScanned), nameof(TicketScanned))]
public interface IEventGrainEvents
{
    public record struct EventCreated(string Name) : IEventGrainEvents;

    public record struct TicketsAdded(NewTicket NewTicket) : IEventGrainEvents;

    public record struct TicketScanned(string TicketId) : IEventGrainEvents;
}