using MassTransit;

namespace Messaging
{
    public interface ITableBooked
    {
        public Guid OrderId { get; }
        public Guid ClientId { get; }
        public Dish? PreOrder { get; }
        public int? TableId { get; }
    }

    public class TableBooked : ITableBooked
    {
        public Guid OrderId { get; }
        public Guid ClientId { get; }
        public Dish? PreOrder { get; }
        public int? TableId { get; }

        public TableBooked(Guid clientId, int? tableId, Dish? preOrder = null)
        {
            OrderId = NewId.NextGuid();
            ClientId = clientId;
            PreOrder = preOrder;
            TableId = tableId;
        }
    }
}
