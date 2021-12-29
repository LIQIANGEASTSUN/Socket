using System;
using System.Collections.Generic;
using System.Text;

namespace Network
{
    class TCPClientController : IipPort
    {
        private TCPClient _tcpClient;
        public TCPClientController()
        {
            _tcpClient = new TCPClient();
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
            Console.WriteLine("请输入要发送的消息ID");
            string cmdID = Console.ReadLine();
            int id = 0;
            int.TryParse(cmdID, out id);

            Console.WriteLine("请输入要发型的消息内容");
            string msg = Console.ReadLine();

            _tcpClient.SendMessage(id, msg);
        }
    }

}