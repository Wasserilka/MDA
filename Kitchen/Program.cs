using System.Text;
using Microsoft.Extensions.Hosting;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Kitchen;
using Kitchen.Consumers;

Console.OutputEncoding = Encoding.UTF8;
CreateHostBuilder(args).Build().Run();

static IHostBuilder CreateHostBuilder(string[] args) =>
Host.CreateDefaultBuilder(args).ConfigureServices((hostContext, services) =>
{
    services.AddMassTransit(x =>
    {
        x.AddConsumer<KitchenTableBookedConsumer>();
        x.UsingRabbitMq((context, cfg) =>
        {
            cfg.ConfigureEndpoints(context);
        });
    });

    services.AddSingleton<Accidents>();
    services.AddSingleton<KitchenState>();
    services.AddHostedService<Worker>();
});