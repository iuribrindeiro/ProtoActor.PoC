using System.Text.Json;
using JasperFx.CodeGeneration;
using Marten;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Proto;
using Proto.Cluster;
using Proto.OpenTelemetry;
using Proto.Remote.HealthChecks;
using Weasel.Core;

var builder = WebApplication.CreateBuilder(args);

var tracingOtlpEndpoint = "http://localhost:4317";

builder.Services.AddMarten(options =>
{
    // Establish the connection string to your Marten database
    options.Connection(builder.Configuration.GetConnectionString("Default")!);

    // Specify that we want to use STJ as our serializer
    options.UseSystemTextJsonForSerialization();

    // If we're running in development mode, let Marten just take care
    // of all necessary schema building and patching behind the scenes
    options.AutoCreateSchemaObjects = AutoCreate.None;
    options.GeneratedCodeMode = TypeLoadMode.Auto;
});

// Configure OpenTelemetry Resources with the application name
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


builder.Services.AddActorSystem(builder.Configuration);
builder.Services.AddHostedService<ActorSystemClusterHostedService>();
builder.Services.ConfigureHttpJsonOptions(e =>
{
    e.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

builder.Services
    .AddHealthChecks()
    .AddCheck<ActorSystemHealthCheck>("actor-system");


var app = builder.Build();

Log.SetLoggerFactory(app.Services.GetRequiredService<ILoggerFactory>());

app.MapGet("/diagnostics", handler: (ActorSystem system)
    => system.Diagnostics.GetDiagnostics());

app.MapHealthChecks("/health");

app.Run();

public class ActorSystemClusterHostedService(ActorSystem actorSystem, ILogger<ActorSystemClusterHostedService> logger)
    : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting a cluster member");

        await actorSystem
            .Cluster()
            .StartMemberAsync();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Shutting down a cluster member");

        await actorSystem
            .Cluster()
            .ShutdownAsync();
    }
}

public record struct AddTicketsInput(string EventId, AddTicketInput[] Tickets);

public record struct AddTicketInput(string Id, string Seat, string Gate);

public record struct CreateEventInput(string Id, string Name);

public record struct Ticket(string Id, string Seat, string Gate, string EvnetId);

public record struct Event(string Id, string Name, Ticket[]? Tickets = null)
{
    public Ticket[] Tickets { get; init; } = Tickets ?? [];
}