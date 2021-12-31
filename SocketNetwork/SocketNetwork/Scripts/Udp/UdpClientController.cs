using System;
using System.Collections.Generic;
using System.Text;

namespace Network
{
    class UdpClientController : INetwork
    {
        private UdpClient _udpClient;
        private NetworkData _networkData;

        public UdpClientController()
        {
            _udpClient = new UdpClient();
        }

        public void SetData(NetworkData networkData)
        {
            _networkData = networkData;
        }

        public void Start()
        {
            _udpClient.StartBind(_networkData.localIp, _networkData.localPort);
            //while (true)
            //{
            //    Input();
            //}
        }

        public void Send(string msg)
        {
            _udpClient.Send(_networkData.uid, _networkData.cmdID, _networkData.remoteIp, _networkData.remotePort, msg);
        }

        private void Input()
        {
            Console.WriteLine("请输入UID");
            string uid = Console.ReadLine();

            Console.WriteLine("请输入cmdID");
            string cmdID = Console.ReadLine();

            Console.WriteLine("请输入消息内容");
            string msg = Console.ReadLine();

            _udpClient.Send(int.Parse(uid), int.Parse(cmdID), _networkData.remoteIp, _networkData.remotePort, msg);
        }
    }
}
