using System;
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
            _tcpClient.SetReceiveCallBack(ReceiveComplete);
        }

        public void Send(string msg)
        {
            byte[] byteData = Encoding.Default.GetBytes(msg);
            _tcpClient.Send(_networkData.uid, _networkData.cmdID, _networkData.queueId++, byteData);
        }

        private void ReceiveComplete(int uid, int cmdID, int queueId, byte[] byteData)
        {
            string content = Encoding.Default.GetString(byteData);
            Debug.Log("uid : " + uid + "    cmdID : " + cmdID + "   queueId:" + queueId + "   content : " + content);
            if (null != NetworkController.receiveMessage)
            {
                NetworkController.receiveMessage(uid, cmdID, queueId, content);
            }
        }
    }
}