using Messaging;
using System.Collections.Concurrent;

namespace Notification
{
    public class Notifier
    {
        private readonly ConcurrentDictionary<Guid, Tuple<Guid?, Accepted, int?, Dish?>> _state = new();

        public void Accept(Guid orderId, Accepted accepted, int? tableId = null, Dish? dish = null, Guid? clientId = null)
        {
            _state.AddOrUpdate(orderId, new Tuple<Guid?, Accepted, int?, Dish?>(clientId, accepted, tableId, dish), (guid, oldValue) => 
            new Tuple<Guid?, Accepted, int?, Dish?>(oldValue.Item1 ?? clientId, oldValue.Item2 | accepted, oldValue.Item3 ?? tableId, oldValue.Item4 ?? dish));

            Notify(orderId);
        }

        private void Notify(Guid orderId)
        {
            var booking = _state[orderId];

            switch (booking.Item2)
            {
                case Accepted.Confirmed:
                    Console.WriteLine($"{DateTime.Now:T} Успешно забронирован столик {booking.Item3} для клиента {booking.Item1}.");
                    _state.Remove(orderId, out _);
                    break;
                case Accepted.Rejected:
                case Accepted.BookingRejected:
                    Console.WriteLine($"{DateTime.Now:T} Гость {booking.Item1}, к сожалению, свободного столика нет.");
                    _state.Remove(orderId, out _);
                    break;
                case Accepted.KitchenRejected:
                    Console.WriteLine($"{DateTime.Now:T} Гость {booking.Item1}, к сожалению, кухня не готова принять заказ.");
                    _state.Remove(orderId, out _);
                    break;
                case Accepted.Kitchen:
                case Accepted.Booking:
                    break;
                default:
                    break;
            }
        }

        public void Notify(Guid clientId, int tableid, Accident accident, Dish? dish = null)
        {
            switch (accident)
            {
                case Accident.Broken:
                    Console.WriteLine($"{DateTime.Now:T} Гость {clientId}, к сожалению, бронирование столика {tableid} пришлось отменить из-за поломки на кухне.");
                    break;
                case Accident.DishStopped:
                    Console.WriteLine($"{DateTime.Now:T} Гость {clientId}, к сожалению, бронирование столика {tableid} пришлось отменить из-за отмены блюда {dish}.");
                    break;
                default:
                    break;
            }
        }
    }
}
