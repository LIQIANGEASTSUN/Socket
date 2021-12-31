using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Threading;

namespace Network
{
    class UdpClient : IipPort
    {

        // public IAsyncResult BeginSendTo(byte[] buffer, int offset, int size, SocketFlags socketFlags, EndPoint remoteEP, AsyncCallback callback, object state);

        // public IAsyncResult BeginReceiveFrom(byte[] buffer, int offset, int size, SocketFlags socketFlags, ref EndPoint remoteEP, AsyncCallback callback, object state);

        private string _ip = "10.1.10.103";
        private int _prot = 8000;
        private Socket _socket;
        private Receive _receive;

        public UdpClient()
        {
            _receive = new Receive();
            _receive.SetCompleteCallBack(ReceiveComplete);
        }

        public void SetIpAndPort(string ip, int port)
        {
            _ip = ip;
            _prot = port;
        }

        public void StartBind()
        {
            IPAddress ipAddress = IPAddress.Parse(_ip);
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, _prot);
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _socket.Bind(ipEndPoint);

            BeginReceive();
        }

        private void BeginReceive()
        {
            StateUdpObject state = new StateUdpObject();
            state.workSocket = _socket;
            _socket.BeginReceiveFrom(state.buffer, 0, StateObject.bufferSize, SocketFlags.None, ref state.remote, ReceiveCallBack, state);
        }

        private void ReceiveCallBack(IAsyncResult ar)
        {
            StateUdpObject state = ar as StateUdpObject;
            try
            {
                int read = state.workSocket.EndReceiveFrom(ar, ref state.remote);
                _receive.ReceiveMessage(state.buffer);
                BeginReceive();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Send(int uid, int cmdID, string ip, int port, string msg)
        {
            byte[] bytes = Encoding.Default.GetBytes(msg);
            Send(uid, cmdID, ip, port, bytes);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="bytes"></param>
        public void Send(int uid, int cmdID, string ip, int port, byte[] bytes)
        {
            try
            {
                byte[] uidBytes = BitConverter.GetBytes(uid);
                byte[] cmdBytes = BitConverter.GetBytes(cmdID);
                int length = uidBytes.Length + cmdBytes.Length + bytes.Length;  // uid + cmd + 内容
                byte[] lengthBytes = BitConverter.GetBytes(length);

                ByteBuffer byteBuffer = new ByteBuffer((lengthBytes.Length + length));
                byteBuffer.WriteInt(length);
                byteBuffer.WriteBytes(uidBytes);
                byteBuffer.WriteBytes(cmdBytes);
                byteBuffer.WriteBytes(bytes);

                StateUdpObject stateObject = new StateUdpObject();
                stateObject.workSocket = _socket;

                IPAddress ipAddress = IPAddress.Parse(ip);
                IPEndPoint remote = new IPEndPoint(ipAddress, port);

                byte[] byteData = byteBuffer.GetData();
                // 异步发送数据到指定套接字所代表的网络设备
                _socket.BeginSendTo(byteData, 0, byteData.Length, SocketFlags.None, remote, SendCallBack, stateObject);
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }

        /// <summary>
        /// 发送消息回调
        /// </summary>
        /// <param name="ar"></param>
        private void SendCallBack(IAsyncResult ar)
        {
            try
            {
                StateObject state = (StateObject)ar.AsyncState;
                Socket socket = state.workSocket;
                socket.EndSendTo(ar);
            }
            catch (SocketException se)
            {
                Debug.Log(se.Message);
            }
        }

        private void ReceiveComplete(int uid, int cmdID, byte[] byteData)
        {
            string content = Encoding.ASCII.GetString(byteData);
            Debug.Log("uid : " + uid + "    cmdID : " + cmdID + "   content : " + content);
        }

    }
}
