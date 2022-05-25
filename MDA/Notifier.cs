using Booking.Models;
using MassTransit;
using Messaging;

namespace Booking
{
    public class Notifier
    {
        private readonly IBus _bus;

        public Notifier(IBus bus)
        {
            _bus = bus;
        }

        public async void PublishBookCancelled(Guid clientId, int tableId, Accident accident, Dish? dish = null) => 
            await _bus.Publish<IBookCancelled>(new BookCancelled(accident, dish, clientId, tableId));

        public async void PublishTableBooked(Guid clientId, Table? result, Dish? dish) => 
            await _bus.Publish<ITableBooked>(new TableBooked(clientId, result is null ? null : result.Id, dish));
    }
}
