using MassTransit;
using Messaging;

namespace Booking.Models
{
    public class Restaurant
    {
        private readonly List<Table> _tables = new();

        public Restaurant()
        {
            for (int i = 1; i <= 10; i++)
            {
                _tables.Add(new Table(i));
            }
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

        public void CancelBook(ConsumeContext<IKitchenAccident> context)
        {
            switch (context.Message.Accident)
            {
                case Accident.Broken:
                    foreach (var item in _tables) 
                    { 
                        if (item.State == State.Booked)
                        {
                            item.SetState(State.Free);
                            context.Publish<IBookCancelled>(new BookCancelled(Accident.Broken, item.Dish, item.ClientId, item.Id));
                        }
                    }
                    Console.WriteLine($"{DateTime.Now:T} Все бронирования отменены.");
                    break;
                case Accident.DishStopped:
                    foreach (var item in _tables)
                    {
                        if (item.Dish == context.Message.Dish)
                        {
                            item.SetState(State.Free);
                            context.Publish<IBookCancelled>(new BookCancelled(Accident.DishStopped, item.Dish, item.ClientId, item.Id));
                            Console.WriteLine($"{DateTime.Now:T} Бронирование столика {item.Id} отменено из-за отмены блюда {context.Message.Dish}.");
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
