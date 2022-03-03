using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDA.Models
{
    public class Restaurant
    {
        private readonly List<Table> Tables = new();

        public Restaurant()
        {
            for (int i = 1; i <= 10; i++)
            {
                Tables.Add(new Table(i));
            }
        }

        public void BookFreeTable(int PersonsCount)
        {
            Console.WriteLine("Добрый день! Подождите секунду, я подберу столик и подтвержу вашу бронь, оставайтесь на линии.");

            var table = Tables.FirstOrDefault(t => t.SeatsCount >= PersonsCount && t.State == State.Free);

            Thread.Sleep(5000);
            table?.SetState(State.Booked);

            Console.WriteLine(table is null
                ? "К сожалению, сейчас все столики заняты"
                : $"Готово! Ваш столик под номером {table.Id}");
        }

        public void BookFreeTableAsync(int PersonsCount)
        {
            Console.WriteLine("Добрый день! Подождите секунду, я подберу столик и подтвержу вашу бронь, оставайтесь на линии.");
            Task.Run(async () =>
            {
                var table = Tables.FirstOrDefault(t => t.SeatsCount >= PersonsCount && t.State == State.Free);

                await Task.Delay(5000);
                table?.SetState(State.Booked);


                Console.WriteLine(table is null
                    ? "УВЕДОМЛЕНИЕ: К сожалению, сейчас все столики заняты"
                    : $"УВЕДОМЛЕНИЕ: Готово! Ваш столик под номером {table.Id}");
            });
        }
    }
}
