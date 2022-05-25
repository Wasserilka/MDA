using Messaging;

namespace Kitchen
{
    public class KitchenState
    {
        private readonly List<Dish?> _dishStopList = new();
        private bool _isKitchenBroken = false;

        public List<Dish?> DishStopList { get { return _dishStopList; } }
        public bool IsKitchenBroken 
        { 
            get { return _isKitchenBroken; } 
            set { _isKitchenBroken = value; } 
        }

        public bool IsDishStopped(Dish? dish) => DishStopList.Contains(dish);
    }
}
