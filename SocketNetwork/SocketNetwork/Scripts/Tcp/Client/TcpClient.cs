using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Threading;

namespace Network
{
    class TcpClient
    {
        private Socket clientSocket = null;

        private ManualResetEvent connectDone = new ManualResetEvent(false);
        private ManualResetEvent sendDone = new ManualResetEvent(false);
        private ManualResetEvent receiveDone = new ManualResetEvent(false);

        private TcpReceive _tcpReceive;
        public TcpClient()
        {
            _tcpReceive = new TcpReceive();
            _tcpReceive.SetCompleteCallBack(ReceiveComplete);
        }

        public void StartConnect(string ip, int port)
        {
            IPAddress ipAddress = IPAddress.Parse(ip);
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.SendTimeout = 20000;

            try
            {
                StateObject state = new StateObject();
                state.workSocket = clientSocket;
                // Connect to the remote endpoint.  
                state.workSocket.BeginConnect(ipEndPoint, ConnectCallBack, state);  // 异步连接

                // 用于暂停主线程的执行，并在可以继续执行时发出信号
                //connectDone.WaitOne();

                Receive(clientSocket);
                //receiveDone.WaitOne();
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
                //connectDone.Set();
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
                state.workSocket.BeginReceive(state.buffer, 0, StateObject.bufferSize, SocketFlags.None, new AsyncCallback(ReceiveCallBack), state);
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
                    _tcpReceive.ReceiveMessage(state.buffer);
                    Receive(client);
                }
                else
                {
                    // All the data has arrived; put it in response. 
                    _tcpReceive.ReceiveMessage(new byte[] { });
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
                byte[] bytesData = SendData.ToTcpByte(uid, cmdID, bytes);
                StateObject stateObject = new StateObject();
                stateObject.workSocket = clientSocket;
                // 异步发送数据到指定套接字所代表的网络设备
                stateObject.workSocket.BeginSend(bytesData, 0, bytesData.Length, SocketFlags.None, SendCallBack, stateObject);
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
            if (null != NetworkController.receiveMessage)
            {
                NetworkController.receiveMessage(uid, cmdID, content);
            }
            Debug.Log("uid : " + uid + "    cmdID : " + cmdID + "   content : " + content);
        }
    }
}