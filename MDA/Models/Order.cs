using Messaging;

namespace Booking.Models
{
    public class Order
    {
        public Dish? Dish { get; }
        public int PersonsCount { get; }

        public Order()
        {
            var dishValues = Enum.GetValues(typeof(Dish));
            Dish = (Dish?)dishValues.GetValue(new Random().Next(dishValues.Length));
            PersonsCount = new Random().Next(1, 6);
        }
    }
}
