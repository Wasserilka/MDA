using MassTransit;
using Messaging;

namespace Notification.Consumers
{
    public class NotifierBookCancelledConsumer : IConsumer<IBookCancelled>
    {
        private readonly Notifier _notifier;

        public NotifierBookCancelledConsumer(Notifier notifier)
        {
            _notifier = notifier;
        }
        
        public Task Consume(ConsumeContext<IBookCancelled> context)
        {
            var message = context.Message;

            _notifier.Notify(message.ClientId, message.TableId, message.Accident, message.Dish);

            return Task.CompletedTask;
        }
    }
}
