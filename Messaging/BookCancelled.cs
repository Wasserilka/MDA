namespace Messaging
{
    public interface IBookCancelled
    {
        public Accident Accident { get; }
        public Dish? Dish { get; }
        public Guid ClientId { get; }
        public int TableId { get; }
    }
    public class BookCancelled : IBookCancelled
    {
        public Accident Accident { get; }
        public Dish? Dish { get; }
        public Guid ClientId { get; }
        public int TableId { get; }

        public BookCancelled(Accident accident, Dish? dish, Guid clientId, int tableId)
        {
            Accident = accident;
            Dish = dish;
            ClientId = clientId;
            TableId = tableId;
        }
    }
}
