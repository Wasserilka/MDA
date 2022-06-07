using Booking.Models;
using MassTransit;
using Messaging;

namespace Booking.Consumers
{
    public class BookingKitchenAccidentConsumer : IConsumer<IKitchenAccident>
    {
        private readonly Restaurant _restaurant;

        public BookingKitchenAccidentConsumer(Restaurant restaurant)
        {
            _restaurant = restaurant;
        }
        
        public Task Consume(ConsumeContext<IKitchenAccident> context)
        {
            _restaurant.CancelBook(context);

            return Task.CompletedTask;
        }
    }
}
