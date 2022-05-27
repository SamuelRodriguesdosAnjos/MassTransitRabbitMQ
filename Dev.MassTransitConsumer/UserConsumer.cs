using MassTransit;

namespace Dev.MassTransitConsumer
{
    public class UserConsumer : IConsumer<UserEvent>
    {
        private readonly ILogger<UserConsumer> _logger;

        public UserConsumer(ILogger<UserConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<UserEvent> context)
        {
            _logger.LogInformation($"User successfully sent: {context.Message.Name}");
        }
    }    
}
