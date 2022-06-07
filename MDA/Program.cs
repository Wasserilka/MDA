using Booking.Models;
using System.Text;
using Booking;
using Microsoft.Extensions.Hosting;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Booking.Consumers;

Console.OutputEncoding = Encoding.UTF8;
CreateHostBuilder(args).Build().Run();

static IHostBuilder CreateHostBuilder(string[] args) =>
Host.CreateDefaultBuilder(args).ConfigureServices((hostContext, services) =>
{
    services.AddMassTransit(x =>
    {
        x.AddConsumer<BookingKitchenAccidentConsumer>();
        x.AddConsumer<BookingKitchenReadyConsumer>();
        x.UsingRabbitMq((context, cfg) =>
        {
            cfg.ConfigureEndpoints(context);
        });
    });

    services.AddSingleton<Restaurant>();
    services.AddHostedService<Worker>();
});
