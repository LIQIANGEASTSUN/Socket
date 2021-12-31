using System;
using System.Collections.Generic;
using System.Text;

namespace Network
{
    class UdpClientController : IipPort
    {
        private UDPClient _udpClient;

        private string _remoteIp;
        private int _remotePort = 0;
        public UdpClientController()
        {
            _udpClient = new UDPClient();
        }

        public void SetIpAndPort(string ip, int port)
        {
            _udpClient.SetIpAndPort(ip, port);
        }

        public void Init()
        {
            _udpClient.StartBind();

            Console.WriteLine("请输入远端IP");
            _remoteIp = Console.ReadLine();
            Console.WriteLine("请输入远端端口");
            _remotePort = int.Parse(Console.ReadLine());

            while (true)
            {
                Input();
            }
        }

        private void Input()
        {
            Console.WriteLine("请输入UID");
            string uid = Console.ReadLine();

            Console.WriteLine("请输入cmdID");
            string cmdID = Console.ReadLine();

            Console.WriteLine("请输入消息内容");
            string msg = Console.ReadLine();

            _udpClient.Send(int.Parse(uid), int.Parse(cmdID), _remoteIp, _remotePort, msg);
        }
    }
}
