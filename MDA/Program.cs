using MDA.Models;
using System.Diagnostics;
using System.Text;
using System;

Console.OutputEncoding = Encoding.UTF8;
var rest = new Restaurant();
while (true)
{
    await Task.Delay(3000);

    var strBuilder = new StringBuilder();
    strBuilder.Append("Привет! Желаете забронировать столик или снять бронь?");
    strBuilder.Append("\n1 - забронировать, мы уведомим вас по смс (асинхронно).");
    strBuilder.Append("\n2 - забронировать, подождите на линии, мы вас оповестим (синхронно).");
    strBuilder.Append("\n3 - снять бронь, мы уведомим вас по смс (асинхронно).");
    strBuilder.Append("\n4 - снять бронь, подождите на линии, мы вас оповестим (синхронно).");

    Console.WriteLine(strBuilder.ToString());

    var stopWatch = new Stopwatch();
    stopWatch.Start();

    var choice = 1;
    var attribute = 2;
    switch (choice)
    {
        case 1:
            rest.BookFreeTableAsync(attribute);
            break;
        case 2:
            rest.BookFreeTable(attribute);
            break;
        case 3:
            rest.FreeBookedTableAsync(attribute);
            break;
        case 4:
            rest.FreeBookedTable(attribute);
            break;
        default:
            break;
    }

    Console.WriteLine("Спасибо за обращение!");
    stopWatch.Stop();
    var ts = stopWatch.Elapsed;
    Console.WriteLine($"{ts.Seconds:00}:{ts.Milliseconds:00}");
}
