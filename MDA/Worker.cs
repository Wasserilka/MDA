using Booking.Models;
using MassTransit;
using Messaging;
using Microsoft.Extensions.Hosting;
using System.Text;

namespace Booking
{
    public class Worker : BackgroundService
    {
        private readonly Restaurant _restaurant;
        private readonly IBus _bus;

        public Worker(Restaurant restaurant, IBus bus)
        {
            _restaurant = restaurant;
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            Console.OutputEncoding = Encoding.UTF8;
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(10000, cancellationToken);

                Console.WriteLine($"{DateTime.Now:T} Привет! Желаете забронировать столик?");

                var order = new Order();
                var clientId = NewId.NextGuid();

                var result = await _restaurant.BookFreeTableAsync(order.PersonsCount, order.Dish, clientId);
                await _bus.Publish<ITableBooked>(new TableBooked(clientId, result is null ? null : result.Id, order.Dish));

                Console.WriteLine($"{DateTime.Now:T} Заказ столика: гость {clientId}, количество человек: {order.PersonsCount}, блюдо: {order.Dish}.");
            }
        }
    }
}
