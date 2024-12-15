using Azure.Monitor.OpenTelemetry.AspNetCore;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenTelemetry()
    .ConfigureResource(builder => 
        builder
        .AddService(serviceName: "OTel.NET Getting Started"))
    .WithMetrics(builder => builder
         .AddMeter("System.Runtime")
        .AddAspNetCoreInstrumentation()
        .AddConsoleExporter((exporterOptions, metricReaderOptions) =>
            {
                metricReaderOptions.PeriodicExportingMetricReaderOptions.ExportIntervalMilliseconds = 3000;
            })).UseAzureMonitor();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
