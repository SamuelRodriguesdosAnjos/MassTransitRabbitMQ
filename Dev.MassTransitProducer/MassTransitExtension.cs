using MassTransit;

public static class MassTransitExtension
{
    public static IServiceCollection AddMassTransitExtension(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            //Informar para o Mass Transit criar o nome de exchange, queue, e etc, no padrão KebabCase
            x.SetKebabCaseEndpointNameFormatter();

            //Trabalhar com agendamentos de mensagens
            x.AddDelayedMessageScheduler();
            
            //Configura o provider do RabbitMQ
            x.UsingRabbitMq((ctx, cfg) =>
            {        
                cfg.Host(configuration.GetConnectionString("RabbitMQ"));

                //Defini o prefixo dos padrões de nomes como "dev"
                cfg.ConfigureEndpoints(ctx, new KebabCaseEndpointNameFormatter("dev", false));

                cfg.UseDelayedMessageScheduler();                

                //Tentativas a cada 5 segundos
                cfg.UseMessageRetry(retry => { retry.Interval(3, TimeSpan.FromSeconds(5)); });
            });
        });

        return services;
    }
}