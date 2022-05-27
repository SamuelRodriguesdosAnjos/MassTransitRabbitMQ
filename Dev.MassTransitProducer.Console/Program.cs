using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

await Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddMassTransit(x =>
        {
            //Informar para o Mass Transit criar o nome de exchange, queue, e etc, no padrão KebabCase
            x.SetKebabCaseEndpointNameFormatter();

            //Configura o provider do RabbitMQ
            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(context.Configuration.GetConnectionString("RabbitMQ"));

                //Defini o prefixo dos padrões de nomes como "dev"
                cfg.ConfigureEndpoints(ctx, new KebabCaseEndpointNameFormatter("dev", false));

                //Tentativas a cada 5 segundos
                cfg.UseMessageRetry(retry => { retry.Interval(3, TimeSpan.FromSeconds(5)); });
            });
        });

        services.AddHostedService<Worker>();
    })
    .Build()
    .RunAsync();
