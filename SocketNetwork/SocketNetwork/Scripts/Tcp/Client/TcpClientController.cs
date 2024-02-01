using System;
using System.Collections.Generic;
using System.Text;

namespace Network
{
    class TcpClientController : INetwork
    {
        private TcpClient _tcpClient;
        private NetworkData _networkData;
        public TcpClientController()
        {
            _tcpClient = new TcpClient();
        }

        public void SetData(NetworkData networkData)
        {
            _networkData = networkData;
        }

        public void Start()
        {
            _tcpClient.StartConnect(_networkData.remoteIp, _networkData.remotePort);
            //while (true)
            //{
            //    Input();
            //}
        }

        public void Send(string msg)
        {
            byte[] byteData = Encoding.Default.GetBytes(msg);
            _tcpClient.Send(_networkData.uid, _networkData.cmdID, _networkData.queueId++, byteData);
        }

        private void Input()
        {
            Console.WriteLine("请输入UID");
            string uid = Console.ReadLine();
        
            Console.WriteLine("请输入cmdID");
            string cmdID = Console.ReadLine();

            Console.WriteLine("请输入消息内容");
            string msg = Console.ReadLine();

            byte[] byteData = Encoding.Default.GetBytes(msg);

            _tcpClient.Send(int.Parse(uid), int.Parse(cmdID), _networkData.queueId++, byteData);
        }
    }

}