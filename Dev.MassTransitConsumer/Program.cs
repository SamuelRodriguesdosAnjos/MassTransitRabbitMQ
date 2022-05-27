var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

builder.Services.AddMassTransitExtension(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();

await app.RunAsync();

Console.WriteLine("Waiting for new messages...");