using Messaging;

namespace Kitchen
{
    public class Accidents
    {
        private readonly Notifier _notifier;
        private readonly KitchenState _kitchenState;

        public Accidents(Notifier notifier, KitchenState kitchenState)
        {
            _notifier = notifier;
            _kitchenState = kitchenState;
        }

        public void BreakTheKitchen()
        {
            _kitchenState.IsKitchenBroken = true;
            _notifier.PublishKitchenAccident();
            Console.WriteLine($"{DateTime.Now:T} Кухня снова сломалась!");

            Task.Run(async () =>
            {
                while (_kitchenState.IsKitchenBroken == true)
                {
                    await Task.Delay(2000);
                    if (new Random().Next(1, 101) <= 10)
                    {
                        _kitchenState.IsKitchenBroken = false;
                        Console.WriteLine($"{DateTime.Now:T} Кухня снова работает.");
                    }
                }
            });
        }

        public void ChangeDishState(Dish? dish)
        {
            if (_kitchenState.DishStopList.Contains(dish))
            {
                _kitchenState.DishStopList.Remove(dish);
                Console.WriteLine($"{DateTime.Now:T} Блюдо {dish} убрано из стоп-листа.");
            }
            else
            {
                _kitchenState.DishStopList.Add(dish);
                _notifier.PublishKitchenAccident(dish);
                Console.WriteLine($"{DateTime.Now:T} Добавлено блюдо в стоп-лист: {dish}");
            }
        }
    }
}
