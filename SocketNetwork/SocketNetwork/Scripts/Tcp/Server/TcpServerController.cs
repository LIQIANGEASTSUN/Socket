
namespace Network
{
    class TcpServerController : INetwork
    {
        private TcpServer _tcpServer;
        private NetworkData _networkData;

        public TcpServerController()
        {
            _tcpServer = new TcpServer();
        }

        public void SetData(NetworkData networkData)
        {
            _networkData = networkData;
        }

        public void Start()
        {
            _tcpServer.Start(_networkData.localIp, _networkData.localPort);
        }

        public void Send(string msg)
        {
            _tcpServer.Send(_networkData.uid, _networkData.cmdID, msg);
        }
    }
}
