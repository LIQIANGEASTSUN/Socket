using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Threading;

namespace Network
{
    class TCPClient : IipPort
    {
        private Socket clientSocket = null;
        private string _ip = "10.1.10.103";
        private int _prot = 8000;

        private ManualResetEvent connectDone = new ManualResetEvent(false);
        private ManualResetEvent sendDone = new ManualResetEvent(false);
        private ManualResetEvent receiveDone = new ManualResetEvent(false);

        private Receive _receive;
        public TCPClient()
        {
            _receive = new Receive();
            _receive.SetCompleteCallBack(ReceiveComplete);
        }

        public void SetIpAndPort(string ip, int port)
        {
            _ip = ip;
            _prot = port;
        }

        public void StartConnect()
        {
            IPAddress ipAddress = IPAddress.Parse(_ip);
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, _prot);
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.SendTimeout = 20000;

            try
            {
                StateObject state = new StateObject();
                state.workSocket = clientSocket;
                // Connect to the remote endpoint.  
                state.workSocket.BeginConnect(ipEndPoint, ConnectCallBack, state);  // 异步连接

                // 用于暂停主线程的执行，并在可以继续执行时发出信号
                connectDone.WaitOne();

                Receive(clientSocket);
                receiveDone.WaitOne();
            }
            catch (Exception ex)
            {
                Debug.Log("连接服务器失败，请回车退出:" + ex.Message);
            }
        }

        // 连接服务器成功
        private void ConnectCallBack(IAsyncResult ar)
        {
            try
            {
                StateObject state = (StateObject)ar.AsyncState;
                Socket socket = state.workSocket;
                socket.EndConnect(ar);
                Debug.Log("连接服务器成功:" + socket.RemoteEndPoint.ToString());

                // 当远程设备可用时，它连接到远程设备，然后通过设置ManualResetEvent 向应用程序线程发送连接已完成的信号connectDone
                // Signal that the connection has been made.
                connectDone.Set();
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        public void Receive(Socket client)
        {
            try
            {
                // Create the state object
                StateObject state = new StateObject();
                state.workSocket = clientSocket;
                // Begin receiving the data from the remote device.  
                state.workSocket.BeginReceive(state.buffer, 0, StateObject.bufferSize, 0, new AsyncCallback(ReceiveCallBack), state);
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
        }

        private void ReceiveCallBack(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client socket
                // from the asynchronous state object. 
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;
                // Read data from the remote device.  
                int bytesRead = client.EndReceive(ar);
                if (bytesRead > 0)
                {
                    _receive.ReceiveMessage(state.buffer);
                    Receive(client);
                }
                else
                {
                    // All the data has arrived; put it in response. 
                    _receive.ReceiveMessage(new byte[] { });
                    receiveDone.Set();
                }
            }
            catch (SocketException se)
            {
                Debug.Log(se.Message);
            }
        }

        public void Send(int uid, int cmdID, string msg)
        {
            byte[] bytes = Encoding.Default.GetBytes(msg);
            Send(uid, cmdID, bytes);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="bytes"></param>
        public void Send(int uid, int cmdID, byte[] bytes)
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

                StateObject stateObject = new StateObject();
                stateObject.workSocket = clientSocket;
                byte[] byteData = byteBuffer.GetData();
                // 异步发送数据到指定套接字所代表的网络设备
                stateObject.workSocket.BeginSend(byteData, 0, byteData.Length, 0, SendCallBack, stateObject);
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
                socket.EndSend(ar);

                // Siganl that all bytes have been set
                sendDone.Set();
            }
            catch (SocketException se)
            {
                Debug.Log(se.Message);
            }
        }

        public void Dispose()
        {
            try
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
                clientSocket.Dispose();//调用的上一个函数
                clientSocket = null;
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }

        private void ReceiveComplete(int uid, int cmdID, byte[] byteData)
        {
            string content = Encoding.ASCII.GetString(byteData);
            Debug.Log("uid : " + uid + "    cmdID : " + cmdID + "   content : " + content);
        }
    }
}