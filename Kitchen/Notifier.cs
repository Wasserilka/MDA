using MassTransit;
using Messaging;

namespace Kitchen
{
    public class Notifier
    {
        private readonly IBus _bus;
        private readonly KitchenState _kitchenState;
        public Notifier(IBus bus, KitchenState kitchenState)
        {
            _kitchenState = kitchenState;
            _bus = bus;
        }

        public async void PublishKitchenReady(Guid orderId, Guid clientId, Dish? dish)
        {
            var ready = !_kitchenState.IsKitchenBroken & !_kitchenState.IsDishStopped(dish);
            await _bus.Publish<IKitchenReady>(new KitchenReady(orderId, clientId, ready));
        }

        public async void PublishKitchenAccident() => await _bus.Publish<IKitchenAccident>(new KitchenAccident());

        public async void PublishKitchenAccident(Dish? dish) => await _bus.Publish<IKitchenAccident>(new KitchenAccident(dish));
    }
}
