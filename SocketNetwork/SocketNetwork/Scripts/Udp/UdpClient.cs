using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Threading;

namespace Network
{
    class UdpClient
    {
        private Socket _socket;
        private UdpReceive _udpReceive;

        public UdpClient()
        {
            _udpReceive = new UdpReceive();
            _udpReceive.SetCompleteCallBack(ReceiveComplete);
        }

        public void StartBind(string ip, int port)
        {
            IPAddress ipAddress = IPAddress.Parse(ip);
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);
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
            try
            {
                StateUdpObject state = ar.AsyncState as StateUdpObject;
                int read = state.workSocket.EndReceiveFrom(ar, ref state.remote);
                _udpReceive.ReceiveMessage(state.buffer);
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
            //SendData.ToUdpByte(uid, cmdID, ip, port, bytes, SendByte);
        }

        private void SendByte(string ip, int port, byte[] bytes)
        {
            try
            {
                StateUdpObject stateObject = new StateUdpObject();
                stateObject.workSocket = _socket;

                IPAddress ipAddress = IPAddress.Parse(ip);
                IPEndPoint remote = new IPEndPoint(ipAddress, port);

                // 异步发送数据到指定套接字所代表的网络设备
                _socket.BeginSendTo(bytes, 0, bytes.Length, SocketFlags.None, remote, SendCallBack, stateObject);
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
                StateUdpObject state = ar.AsyncState as StateUdpObject;
                Socket socket = state.workSocket;
                socket.EndSendTo(ar);
            }
            catch (SocketException se)
            {
                Debug.Log(se.Message);
            }
        }

        private void ReceiveComplete(int uid, int cmdID, int queueId, byte[] byteData)
        {
            string content = Encoding.Default.GetString(byteData);
            if (null != NetworkController.receiveMessage)
            {
                NetworkController.receiveMessage(uid, cmdID, queueId, content);
            }
            Debug.Log("uid : " + uid + "    cmdID : " + cmdID + "   queueId:" + queueId + "   content : " + content);
        }
    }
}