using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

await Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<UserConsumer>(typeof(UserConsumerDefinition));

            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(context.Configuration.GetConnectionString("RabbitMQ"));

                cfg.ReceiveEndpoint("user-queue", e =>
                {
                    e.ConfigureConsumer<UserConsumer>(ctx);
                });

                cfg.ConfigureEndpoints(ctx);
            });
        });
    })
    .Build()
    .RunAsync();