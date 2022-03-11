using MDA.Communication;
using MDA.Models;
using System.Diagnostics;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
var communicationAgent = new CommunicationAgent();
var rest = new Restaurant(communicationAgent);
while (true)
{
    var strBuilder = new StringBuilder();
    strBuilder.Append("Привет! Желаете забронировать столик или снять бронь?");
    strBuilder.Append("\n1 - забронировать, мы уведомим вас по смс (асинхронно).");
    strBuilder.Append("\n2 - забронировать, подождите на линии, мы вас оповестим (синхронно).");
    strBuilder.Append("\n3 - снять бронь, мы уведомим вас по смс (асинхронно).");
    strBuilder.Append("\n4 - снять бронь, подождите на линии, мы вас оповестим (синхронно).");

    Console.WriteLine(strBuilder.ToString());

    int.TryParse(Console.ReadLine(), out var choice);
    if (choice > 4 || choice < 1)
    {
         communicationAgent.SendMessage("Введите, пожалуйста, любой номер из указанных выше.");
        continue;
    }

    switch (choice)
    {
        case 1:
        case 2:
             communicationAgent.SendMessage("Введите, пожалуйста, количество гостей.");
            break;
        case 3:
        case 4:
             communicationAgent.SendMessage("Введите, пожалуйста, номер столика.");
            break;
        default:
            break;
    }
    int.TryParse(Console.ReadLine(), out var attribute);
    if (attribute < 1)
    {
         communicationAgent.SendMessage("Введите, пожалуйста, положительное число.");
        continue;
    }

    var stopWatch = new Stopwatch();
    stopWatch.Start();
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

    communicationAgent.SendMessage("Спасибо за обращение!");
    stopWatch.Stop();
    var ts = stopWatch.Elapsed;
    Console.WriteLine($"{ts.Seconds:00}:{ts.Milliseconds:00}");
}
