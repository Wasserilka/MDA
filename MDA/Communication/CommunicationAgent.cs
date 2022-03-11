using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDA.Communication
{
    public class CommunicationAgent
    {
        public void SendMessage(string message)
        {
            Task.Run(async () => 
            {
                await Task.Delay(1000);
                Console.WriteLine(message);
            });
        }
    }
}
