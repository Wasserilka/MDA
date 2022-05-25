using Messaging;

namespace Booking.Models
{
    public class Restaurant
    {
        private readonly List<Table> _tables = new();
        private readonly Notifier _notifier;

        public Restaurant(Notifier notifier)
        {
            for (int i = 1; i <= 10; i++)
            {
                _tables.Add(new Table(i));
            }
            _notifier = notifier;
        }

        public void BookFreeTable(int PersonsCount)
        {
            Thread.Sleep(5000);
            var table = _tables.FirstOrDefault(t => t.SeatsCount >= PersonsCount && t.State == State.Free);
            table?.SetState(State.Booked);
        }

        public void FreeBookedTable(int tableId)
        {
            Thread.Sleep(5000);
            var table = _tables.FirstOrDefault(t => t.Id == tableId && t.State == State.Booked);
            table?.SetState(State.Free);
        }

        public async Task<Table?> BookFreeTableAsync(int PersonsCount, Dish? dish, Guid clientId)
        {
            await Task.Delay(5000);
            var table = _tables.FirstOrDefault(t => t.SeatsCount >= PersonsCount && t.State == State.Free);
            table?.SetState(State.Booked);
            table?.SetOrder(dish, clientId);

            return table;
        }

        public async Task BookedTableAsync(int tableId)
        {
            await Task.Delay(5000);
            var table = _tables.FirstOrDefault(t => t.Id == tableId && t.State == State.Booked);
            table?.SetState(State.Free);
        }

        public void CancelBook(Accident accident, Dish? dish = null)
        {
            switch (accident)
            {
                case Accident.Broken:
                    foreach (var item in _tables) 
                    { 
                        if (item.State == State.Booked)
                        {
                            item.SetState(State.Free);
                            _notifier.PublishBookCancelled(item.ClientId, item.Id, Accident.Broken);
                        }
                    }
                    Console.WriteLine($"{DateTime.Now:T} Все бронирования отменены.");
                    break;
                case Accident.DishStopped:
                    foreach (var item in _tables)
                    {
                        if (item.Dish == dish)
                        {
                            item.SetState(State.Free);
                            _notifier.PublishBookCancelled(item.ClientId, item.Id, Accident.DishStopped, dish = dish);
                            Console.WriteLine($"{DateTime.Now:T} Бронирование столика {item.Id} отменено из-за отмены блюда {dish}.");
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public void CancelBook(Guid clientId)
        {
            foreach (var item in _tables)
            {
                if (item.State == State.Booked && item.ClientId == clientId)
                {
                    item.SetState(State.Free);
                    return;
                }
            }
        }
    }
}
