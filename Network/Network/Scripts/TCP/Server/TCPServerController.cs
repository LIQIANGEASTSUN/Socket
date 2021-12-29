using System;
using System.Collections.Generic;
using System.Text;

namespace Network
{
    class TCPServerController : IipPort
    {
        private TCPServer _tcpServer;

        public TCPServerController()
        {
            _tcpServer = new TCPServer();
        }

        public void SetIpAndPort(string ip, int port)
        {
            _tcpServer.SetIpAndPort(ip, port);
        }

        public void Init()
        {
            _tcpServer.Start();
        }
    }
}
