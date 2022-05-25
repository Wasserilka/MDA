using Messaging;
using Microsoft.Extensions.Hosting;
using System.Text;

namespace Kitchen
{
    public class Worker : BackgroundService
    {
        private readonly KitchenState _kitchenState;
        private readonly Accidents _accidents;

        public Worker(KitchenState kitchenState, Accidents accidents)
        {
            _kitchenState = kitchenState;
            _accidents = accidents;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            Console.OutputEncoding = Encoding.UTF8;
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(5000, cancellationToken);

                if (_kitchenState.IsKitchenBroken) { continue; }

                var accident = new Random().Next(0, 101);
                if (accident <= 10) { _accidents.BreakTheKitchen(); }
                else if (accident <= 30)
                {
                    var dishValues = Enum.GetValues(typeof(Dish));
                    _accidents.ChangeDishState((Dish?)dishValues.GetValue(new Random().Next(dishValues.Length)));
                }
            }
        }
    }
}
