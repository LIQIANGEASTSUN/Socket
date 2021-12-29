using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            SocketClient socketClient = new SocketClient();

            socketClient.StartConnect();

            socketClient.ReceiveMessage();

            while (true)
            {
                string meg = Console.ReadLine();
                if (!string.IsNullOrEmpty(meg))
                {
                    socketClient.SnedMessage(meg);
                }
            }

        }
    }
}
