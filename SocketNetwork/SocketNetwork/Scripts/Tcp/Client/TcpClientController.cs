using System;
using System.Collections.Generic;
using System.Text;

namespace Network
{
    class TcpClientController : IipPort
    {
        private TcpClient _tcpClient;
        public TcpClientController()
        {
            _tcpClient = new TcpClient();
        }

        public void SetIpAndPort(string ip, int port)
        {
            _tcpClient.SetIpAndPort(ip, port);
        }

        public void Init()
        {
            _tcpClient.StartConnect();

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

            _tcpClient.Send(int.Parse(uid), int.Parse(cmdID), msg);
        }
    }

}