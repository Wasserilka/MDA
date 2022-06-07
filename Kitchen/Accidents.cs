using MassTransit;
using Messaging;

namespace Kitchen
{
    public class Accidents
    {
        private readonly IBus _bus;
        private readonly KitchenState _kitchenState;

        public Accidents(IBus bus, KitchenState kitchenState)
        {
            _bus = bus;
            _kitchenState = kitchenState;
        }

        public async void BreakTheKitchen()
        {
            _kitchenState.IsKitchenBroken = true;
            await _bus.Publish<IKitchenAccident>(new KitchenAccident());
            Console.WriteLine($"{DateTime.Now:T} Кухня снова сломалась!");

            while (_kitchenState.IsKitchenBroken == true)
            {
                await Task.Delay(2000);
                if (new Random().Next(1, 101) <= 10)
                {
                    _kitchenState.IsKitchenBroken = false;
                    Console.WriteLine($"{DateTime.Now:T} Кухня снова работает.");
                }
            }
        }

        public async void ChangeDishState(Dish? dish)
        {
            if (_kitchenState.DishStopList.Contains(dish))
            {
                _kitchenState.DishStopList.Remove(dish);
                Console.WriteLine($"{DateTime.Now:T} Блюдо {dish} убрано из стоп-листа.");
            }
            else
            {
                _kitchenState.DishStopList.Add(dish);
                await _bus.Publish<IKitchenAccident>(new KitchenAccident(dish));
                Console.WriteLine($"{DateTime.Now:T} Добавлено блюдо в стоп-лист: {dish}");
            }
        }
    }
}
