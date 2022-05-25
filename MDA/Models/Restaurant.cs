using Messaging;

namespace MDA.Models
{
    public class Restaurant
    {
        private readonly List<Table> Tables = new();
        private readonly Producer Producer;

        public Restaurant()
        {
            Producer = new("localhost");

            for (int i = 1; i <= 10; i++)
            {
                Tables.Add(new Table(i));
            }
        }

        public void BookFreeTable(int PersonsCount)
        {
            Producer.Send("Добрый день! Подождите секунду, я подберу столик и подтвержу вашу бронь, оставайтесь на линии.");
            
            Thread.Sleep(5000);
            var table = Tables.FirstOrDefault(t => t.SeatsCount >= PersonsCount && t.State == State.Free);
            table?.SetState(State.Booked);

            Producer.Send(table is null
                ? "К сожалению, сейчас все столики заняты."
                : $"Готово! Ваш столик под номером {table.Id}.");
        }

        public void FreeBookedTable(int tableId)
        {
            Producer.Send("Добрый день! Подождите секунду, я проверю ваш столик и подтвержу отмену брони, оставайтесь на линии.");

            Thread.Sleep(5000);
            var table = Tables.FirstOrDefault(t => t.Id == tableId && t.State == State.Booked);
            table?.SetState(State.Free);

            Producer.Send(table is null
                ? "К сожалению, указанный вами столик не существует или уже свободен."
                : $"Готово! Столик под номером {table.Id} теперь свободен.");
        }

        public void BookFreeTableAsync(int PersonsCount)
        {
            Producer.Send("Добрый день! Подождите секунду, я подберу столик и подтвержу вашу бронь, вам придет уведомление.");

            Task.Run(async () => 
            {
                await Task.Delay(5000);
                var table = Tables.FirstOrDefault(t => t.SeatsCount >= PersonsCount && t.State == State.Free);
                table?.SetState(State.Booked);

                Producer.Send(table is null
                  ? "УВЕДОМЛЕНИЕ: К сожалению, сейчас все столики заняты."
                  : $"УВЕДОМЛЕНИЕ: Готово! Ваш столик под номером {table.Id}.");
            });
        }

        public void FreeBookedTableAsync(int tableId)
        {
            Producer.Send("Добрый день! Подождите секунду, я проверю ваш столик и подтвержу отмену брони, вам придет уведомление.");

            Task.Run(async () =>
            {
                await Task.Delay(5000);
                var table = Tables.FirstOrDefault(t => t.Id == tableId && t.State == State.Booked);
                table?.SetState(State.Free);

                Producer.Send(table is null
                   ? "УВЕДОМЛЕНИЕ: К сожалению, указанный вами столик не существует или уже свободен."
                   : $"УВЕДОМЛЕНИЕ: Готово! Столик под номером {table.Id} теперь свободен.");
            });
        }
    }
}
