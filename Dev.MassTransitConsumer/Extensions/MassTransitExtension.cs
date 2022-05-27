using Dev.MassTransitConsumer;
using MassTransit;

public static class MassTransitExtension
{
    public static IServiceCollection AddMassTransitExtension(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddMassTransit(x =>
        {
            //Trabalhar com agendamentos de mensagens
            x.AddDelayedMessageScheduler();

            //Consumidores
            x.AddConsumer<UserConsumer>(typeof(UserConsumerDefinition));

            //Informar para o Mass Transit criar o nome de exchange, queue, e etc, no padrão KebabCase
            x.SetKebabCaseEndpointNameFormatter();

            //Configura o provider do RabbitMQ
            x.UsingRabbitMq((ctx, cfg) =>
            {        
                cfg.Host(configuration.GetConnectionString("RabbitMQ"));

                cfg.UseDelayedMessageScheduler();

                //Criação de interceptor
                //cfg.ConnectReceiveObserver(new ReceiveObserver());

                // cfg.ReceiveEndpoint("testeFila", ep =>
                // {
                //     ep.PrefetchCount = 10;
                //     ep.UseMessageRetry(r => r.Interval(2, 100));
                //     ep.ConfigureConsumer<UserConsumer>(ctx);
                // });

                cfg.ServiceInstance(instance => {
                    instance.ConfigureJobServiceEndpoints();
                    instance.ConfigureEndpoints(ctx, new KebabCaseEndpointNameFormatter("dev", false));
                });
            });
        });

        service.AddMassTransitHostedService(true);

        return service;
    }
}