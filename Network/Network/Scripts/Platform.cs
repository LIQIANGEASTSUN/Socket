using System;
using System.Collections.Generic;
using System.Text;

namespace Network
{
    class Platform
    {

        public Platform()
        {
            Console.WriteLine("Welcome 输入 1 使用 Tcp， 输入 2 使用 Udp \n");
            string input = Console.ReadLine();
            if (input.CompareTo("1") == 0)
            {
                Tcp();
            }
            else if (input.CompareTo("2") == 0)
            {
                Udp();
            }
        }

        private void Tcp()
        {
            Console.WriteLine("输入 1：做为客户端运行，输入 2：做为服务器运行\n");
            Console.WriteLine("");

            string input = Console.ReadLine();

            Console.WriteLine("请输入IP地址如：10.0.116.245");
            string ip = Console.ReadLine();
            Console.WriteLine("请输入端口如：8000");
            string port = Console.ReadLine();
            Console.WriteLine();

            if (input.CompareTo("1") == 0)
            {
                TCPClientController tcpClientController = new TCPClientController();
                tcpClientController.SetIpAndPort(ip, int.Parse(port));
                tcpClientController.Init();
            }
            else if (input.CompareTo("2") == 0)
            {
                TCPServerController tcpServerController = new TCPServerController();
                tcpServerController.SetIpAndPort(ip, int.Parse(port));
                tcpServerController.Init();
            }
        }

        private void Udp()
        {
            UdpClientController uDPClientController = new UdpClientController();
        }

    }
}
