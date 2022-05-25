namespace Messaging
{
    public interface IKitchenReady
    {
        public Guid OrderId { get; }
        public Guid ClientId { get; }
        public bool Ready { get; }
    }

    public class KitchenReady : IKitchenReady
    {
        public Guid OrderId { get; }
        public Guid ClientId { get; }
        public bool Ready { get; }

        public KitchenReady(Guid orderId, Guid clientId, bool ready)
        {
            OrderId = orderId;
            ClientId = clientId;
            Ready = ready;
        }
    }
}
