using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Users;

public class Worker : BackgroundService
{
    private readonly ISendEndpointProvider _sendEndpointProvider;
    private readonly ILogger<Worker> _logger;

    public Worker(
        ILogger<Worker> logger,
        ISendEndpointProvider sendEndpointProvider
        )
    {
        _logger = logger;
        _sendEndpointProvider = sendEndpointProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var queue = new Uri("rabbitmq://localhost/user-queue");

                var endpoint = await _sendEndpointProvider.GetSendEndpoint(queue);

                var user = new UserEvent
                {
                    Name = "Usuário teste"
                };

                await endpoint.Send(user, stoppingToken);

                _logger.LogInformation($"Published User successfully: {user.Name}");

                await Task.Delay(1000, stoppingToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
