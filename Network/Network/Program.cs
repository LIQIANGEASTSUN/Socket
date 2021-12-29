using System;

namespace Network
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome");
            Console.WriteLine("输入 1：做为客户端运行");
            Console.WriteLine("输入 2：做为服务器运行\n");

            string input = Console.ReadLine();

            Console.WriteLine("请输入IP地址如：10.0.116.245");
            string ip = Console.ReadLine();
            Console.WriteLine("请输入端口如：8000");
            string port = Console.ReadLine();
            Console.WriteLine();

            if (input.CompareTo("1") == 0)
            {
                TcpClientController tcpClientController = new TcpClientController();
                tcpClientController.SetIpAndPort(ip, int.Parse(port));
                tcpClientController.Init();
            }
            else if (input.CompareTo("2") == 0)
            {

            }

            Console.ReadLine();
        }
    }
}
