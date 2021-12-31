using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    class NetworkController
    {
        public static readonly NetworkController Instance = new NetworkController();

        private NetworkData _networkData;
        private INetwork _iNetwork;
        private NetworkController()
        {
            _networkData = new NetworkData();
        }

        public void Start()
        {
            Console.WriteLine("NetworkType:" + _networkData.networkType);
            Console.WriteLine("localIp:" + _networkData.localIp);
            Console.WriteLine("localPort:" + _networkData.localPort);
            Console.WriteLine("remoteIp:" + _networkData.remoteIp);
            Console.WriteLine("remotePort:" + _networkData.remotePort);
            Console.WriteLine("uid:" + _networkData.uid);
            Console.WriteLine("cmdID:" + _networkData.cmdID);

            if (_networkData.networkType == NetworkType.Tcp)
            {
                if (_networkData.networkCS == NetworkCS.Client)
                {
                    _iNetwork = new TcpClientController();
                }
                else
                {
                    _iNetwork = new TcpServerController();
                }
            }
            else
            {
                _iNetwork = new UdpClientController();
            }

            _iNetwork.SetData(_networkData);
            _iNetwork.Start();
        }

        public void Send(string msg)
        {
            Console.WriteLine(msg);
            _iNetwork.Send(msg);
        }

        public NetworkData NetworkData
        {
            get
            {
                return _networkData;
            }
        }

    }
}
