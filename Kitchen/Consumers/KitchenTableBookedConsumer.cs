using MassTransit;
using Messaging;

namespace Kitchen.Consumers
{
    public class KitchenTableBookedConsumer : IConsumer<ITableBooked>
    {
        private readonly KitchenState _kitchenState;
        public KitchenTableBookedConsumer(KitchenState kitchenState)
        {
            _kitchenState = kitchenState;
        }

        public Task Consume(ConsumeContext<ITableBooked> context)
        {
            if (context.Message.TableId is not null)
            {
                var isReady = !_kitchenState.IsKitchenBroken & !_kitchenState.IsDishStopped(context.Message.PreOrder);
                context.Publish<IKitchenReady>(new KitchenReady(context.Message.OrderId, context.Message.ClientId, isReady));
            }

            return Task.CompletedTask;
        }
    }
}
