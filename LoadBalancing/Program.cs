using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.RateLimiting;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateSlimBuilder(args);

var tracingOtlpEndpoint = "http://localhost:4317";

var serviceName = "LoadBalance";

builder.Logging.AddOpenTelemetry(e =>
{
    e.IncludeFormattedMessage = true;
    e.AddOtlpExporter();
});

var myOptions = new MyRateLimiterOptions();
builder.Configuration.GetSection("rate-limit").Bind(myOptions);


builder.Services.AddRequestTimeouts(options =>
{
    options.DefaultPolicy = new RequestTimeoutPolicy { Timeout = TimeSpan.FromMilliseconds(500) };
});

builder.Services.AddRateLimiter(options =>
{
    options.AddSlidingWindowLimiter("sliding", opt =>
    {
        opt.PermitLimit = myOptions.PermitLimit;
        opt.Window = TimeSpan.FromSeconds(myOptions.Window);
        opt.SegmentsPerWindow = myOptions.SegmentsPerWindow;
        opt.QueueProcessingOrder = myOptions.QueueProcessingOrder;
        opt.QueueLimit = myOptions.QueueLimit;
    });

    options.AddFixedWindowLimiter("fixed", opt =>
    {
        opt.PermitLimit = myOptions.PermitLimit;
        opt.Window = TimeSpan.FromSeconds(myOptions.Window);
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = myOptions.QueueLimit;
    });

    options.AddTokenBucketLimiter("bucket", opt =>
    {
        opt.TokenLimit = myOptions.TokenLimit;
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = myOptions.QueueLimit;
        opt.ReplenishmentPeriod = TimeSpan.FromSeconds(myOptions.ReplenishmentPeriod);
        opt.TokensPerPeriod = myOptions.TokensPerPeriod;
        opt.AutoReplenishment = myOptions.AutoReplenishment;
    });

    options.AddConcurrencyLimiter("concurrency", opt =>
    {
        opt.PermitLimit = myOptions.PermitLimit;
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = myOptions.QueueLimit;
    });
});

builder.Services
    .AddResourceMonitoring()
    .AddOpenTelemetry()
    .ConfigureResource(e => e
        .AddService(
            serviceName,
            serviceVersion: System.Reflection.Assembly.GetEntryAssembly()?.GetName().Version?.ToString(3))
        .AddAttributes(new Dictionary<string, object>
        {
            { "host.name", Environment.MachineName }
        }))
    .WithMetrics(metrics => metrics
        .AddAspNetCoreInstrumentation()
        .AddProcessInstrumentation()
        .AddRuntimeInstrumentation()
        .AddOtlpExporter(pro => { pro.Endpoint = new Uri(tracingOtlpEndpoint); }))
    .WithTracing(tracing =>
    {
        tracing
            .AddAspNetCoreInstrumentation()
            .AddSource(serviceName)
            .AddOtlpExporter(otlpOptions => otlpOptions.Endpoint = new Uri(tracingOtlpEndpoint));
    });

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseRateLimiter();

app.MapReverseProxy();

app.MapHealthChecks("health");

app.Run();

public class MyRateLimiterOptions
{
    public int PermitLimit { get; set; }
    public int Window { get; set; }
    public QueueProcessingOrder QueueProcessingOrder { get; set; }
    public int QueueLimit { get; set; }
    public int SegmentsPerWindow { get; set; }
    public int TokenLimit { get; set; }
    public double ReplenishmentPeriod { get; set; }
    public int TokensPerPeriod { get; set; }
    public bool AutoReplenishment { get; set; }
}