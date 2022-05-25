using MassTransit;
using Messaging;

namespace Notification.Consumers
{
    internal class NotifierTableBookedConsumer : IConsumer<ITableBooked>
    {
        private readonly Notifier _notifier;

        public NotifierTableBookedConsumer(Notifier notifier)
        {
            _notifier = notifier;
        }

        public Task Consume(ConsumeContext<ITableBooked> context)
        {
            _notifier.Accept(
                context.Message.OrderId, 
                context.Message.TableId is not null ? Accepted.Booking : Accepted.Rejected, 
                context.Message.TableId,
                context.Message.PreOrder,
                context.Message.ClientId);

            return Task.CompletedTask;
        }
    }
}
