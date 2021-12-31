using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    public delegate void ReceiveMessage(int uid, int cmdId, string msg);
    class NetworkController
    {
        public static readonly NetworkController Instance = new NetworkController();
        public static ReceiveMessage receiveMessage;
        private NetworkData _networkData;
        private INetwork _iNetwork;
        private Queue<string> _receiveQueue = new Queue<string>();
        private NetworkController()
        {
            _networkData = new NetworkData();
            receiveMessage += Receive;
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

        private void Receive(int uid, int cmdID, string msg)
        {
            string str = string.Format("uid:{0}_cmdID_{1}_msg_{2}", uid, cmdID, msg);
            _receiveQueue.Enqueue(str);
            //ReceiveBox.Text = string.Format("msg_{0}", msg);
        }

        public Queue<string> ReceiveQueue
        {
            get
            {
                return _receiveQueue;
            }
        }
    }
}
