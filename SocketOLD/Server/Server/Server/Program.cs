using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("我是服务器");

            SocketManage.Init();

            Console.ReadLine();
        }
    }
}
