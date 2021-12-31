using System;
using System.Collections.Generic;
using System.Text;

namespace Network
{
    class TcpServerController : IipPort
    {
        private TcpServer _tcpServer;

        public TcpServerController()
        {
            _tcpServer = new TcpServer();
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
