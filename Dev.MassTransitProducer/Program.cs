using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Users;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

builder.Services.AddMassTransitExtension(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();

app.MapPost("v1/users/publish", async (
    [FromServices] ILogger<Program> logger, 
    [FromServices] IPublishEndpoint publisher,
    CancellationToken cancellationToken) =>
{
    for (int i = 0; i < 10; i++)
    {
        var user = new UserEvent($"Usuário {i+1}");
        await publisher.Publish(user, cancellationToken);
        logger.LogInformation($"Send user: {user.Name}");
    }

    return Results.Accepted();
});

app.MapPost("v2/users/publish", async (
    [FromServices] ILogger<Program> logger,
    [FromServices] IPublishEndpoint publisher,
    [FromBody] UserEvent user,
    CancellationToken cancellationToken) =>
{
    await publisher.Publish(user, cancellationToken);
    logger.LogInformation($"Send user: {user.Name}");

    return Results.Accepted();
});

app.MapPost("v2/users/publish-scheduler", async (
    [FromServices] ILogger<Program> logger,
    [FromServices] IMessageScheduler publisherScheduler,
    [FromBody] UserEvent user,
    CancellationToken cancellationToken) =>
{
    var date = DateTime.UtcNow.AddMinutes(1);
    await publisherScheduler.SchedulePublish(date, user, cancellationToken);
    logger.LogInformation($"Schedule send user: {user.Name}");

    return Results.Accepted();
});

await app.RunAsync();