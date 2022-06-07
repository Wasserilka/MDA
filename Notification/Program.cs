using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Notification;
using Notification.Consumers;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
CreateHostBuilder(args).Build().Run();

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args).ConfigureServices((hostContext, services) =>
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<NotifierBookCancelledConsumer>();
            x.AddConsumer<NotifierTableBookedConsumer>();
            x.AddConsumer<NotifierKitchenReadyConsumer>();
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        });
        services.AddSingleton<Notifier>();
    });