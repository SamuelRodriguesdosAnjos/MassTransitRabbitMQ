using MassTransit;
using Microsoft.Extensions.Logging;
using Users;

public class UserConsumer : IConsumer<UserEvent>
{
    private readonly ILogger<UserConsumer> _logger;

    public UserConsumer(ILogger<UserConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<UserEvent> context)
    {
        _logger.LogInformation($"Received user successfully: {context.Message.Name}");
    }
}
