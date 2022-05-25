namespace Messaging
{
    public interface IKitchenAccident
    {
        public Accident Accident { get; }
        public Dish? Dish { get; }
    }

    public class KitchenAccident : IKitchenAccident
    {
        public Dish? Dish { get; }
        public Accident Accident { get; }

        public KitchenAccident(Dish? dish)
        {
            Accident = Accident.DishStopped;
            Dish = dish;
        }
        public KitchenAccident()
        {
            Accident = Accident.Broken;
        }
    }
}
