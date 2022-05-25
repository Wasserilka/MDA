using Booking.Models;
using Microsoft.Extensions.Hosting;
using System.Text;

namespace Booking
{
    public class Worker : BackgroundService
    {
        private readonly Restaurant _restaurant;
        private readonly Notifier _notifier;

        public Worker(Restaurant restaurant, Notifier notifier)
        {
            _restaurant = restaurant;
            _notifier = notifier;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            Console.OutputEncoding = Encoding.UTF8;
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(10000, cancellationToken);

                Console.WriteLine($"{DateTime.Now:T} Привет! Желаете забронировать столик?");

                var order = new Order();
                var clientId = Guid.NewGuid();

                var result = await _restaurant.BookFreeTableAsync(order.PersonsCount, order.Dish, clientId);
                _notifier.PublishTableBooked(clientId, result, order.Dish);

                Console.WriteLine($"{DateTime.Now:T} Заказ столика: гость {clientId}, количество человек: {order.PersonsCount}, блюдо: {order.Dish}.");
            }
        }
    }
}
