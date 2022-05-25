using Messaging;
using Microsoft.Extensions.Hosting;
using System.Text;

namespace Notification
{
    public class Worker : BackgroundService
    {
        private readonly Consumer Consumer;

        public Worker()
        {
            Consumer = new Consumer("localhost");
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            Consumer.Receive((sender, args) =>
            {
                var body = args.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Received {message}");
            });
        }
    }
}
