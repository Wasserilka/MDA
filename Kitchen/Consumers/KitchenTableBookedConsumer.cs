using MassTransit;
using Messaging;

namespace Kitchen.Consumers
{
    public class KitchenTableBookedConsumer : IConsumer<ITableBooked>
    {
        private readonly Notifier _notifier;
        public KitchenTableBookedConsumer(Notifier notifier)
        {
            _notifier = notifier;
        }

        public Task Consume(ConsumeContext<ITableBooked> context)
        {
            if (context.Message.TableId is not null)
            {
                _notifier.PublishKitchenReady(context.Message.OrderId, context.Message.ClientId, context.Message.PreOrder);
            }

            return Task.CompletedTask;
        }
    }
}
