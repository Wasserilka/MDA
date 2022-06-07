using MassTransit;
using Messaging;

namespace Notification.Consumers
{
    public class NotifierKitchenReadyConsumer : IConsumer<IKitchenReady>
    {
        private readonly Notifier _notifier;

        public NotifierKitchenReadyConsumer(Notifier notifier)
        {
            _notifier = notifier;
        }
        
        public Task Consume(ConsumeContext<IKitchenReady> context)
        {
            _notifier.Accept(context.Message.OrderId, context.Message.Ready ? Accepted.Kitchen : Accepted.Rejected);

            return Task.CompletedTask;
        }
    }
}
