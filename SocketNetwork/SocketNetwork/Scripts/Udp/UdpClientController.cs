
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
            _udpClient.StartBind(_networkData);
        }

        public void Send(string msg)
        {
            _udpClient.Send(_networkData.uid, _networkData.cmdID, _networkData.remoteIp, _networkData.remotePort, msg);
        }
    }
}
