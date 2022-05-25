using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Notification;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
CreateHostBuilder(args).Build().Run();

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args).ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Worker>();
    });