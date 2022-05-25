using Booking.Models;
using MassTransit;
using Messaging;

namespace Booking.Consumers
{
    public class BookingKitchenReadyConsumer : IConsumer<IKitchenReady>
    {
        private readonly Restaurant _restaurant;

        public BookingKitchenReadyConsumer(Restaurant restaurant)
        {
            _restaurant = restaurant;
        }
        
        public Task Consume(ConsumeContext<IKitchenReady> context)
        {
            if (!context.Message.Ready)
            {
                _restaurant.CancelBook(context.Message.ClientId);
            }

            return Task.CompletedTask;
        }
    }
}
