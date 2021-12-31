using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace Network
{

    class TcpServer : IipPort
    {
        private string _ip = "10.1.10.103";
        private int _prot = 8000;
        private ManualResetEvent allDone = new ManualResetEvent(false);

        private Dictionary<Socket, Receive> _recevieDic = new Dictionary<Socket, Receive>();
        public TcpServer()
        {

        }

        public void SetIpAndPort(string ip, int port)
        {
            _ip = ip;
            _prot = port;
        }

        public void Start()
        {
            IPAddress ipAddress = IPAddress.Parse(_ip);    // 服务器 IP
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, _prot);
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //确定本地端点后，必须使用Bind方法将Socket与该端点关联，并使用Listen方法设置为侦听端点。
            //如果特定地址和端口组合已在使用中，则Bind将引发异常
            try
            {
                listener.Bind(ipEndPoint);
                // 连接队列中最多放置 100 个客户端
                listener.Listen(100);

                StartAccept(listener);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            StartAccept(listener);
        }

        //异步套接字使用系统线程池中的线程来处理传入的连接。一个线程负责接受连接，
        //另一个线程用于处理每个传入的连接，另一个线程负责从连接接收数据。
        //这些可能是同一个线程，具体取决于线程池分配了哪个线程

        // 监听客户端连接
        private void StartAccept(Socket listener)
        {
            try
            {
                while (true)
                {
                    allDone.Reset();
                    listener.BeginAccept(new AsyncCallback(AcceptCallBack), listener);
                    Console.WriteLine("启动监听{0}成功", listener.LocalEndPoint.ToString());
                    allDone.WaitOne();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void AcceptCallBack(IAsyncResult ar)
        {
            allDone.Set();

            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);
            Receive receive = new Receive();
            receive.SetCompleteCallBack(ReceiveComplete);
            _recevieDic[handler] = receive;

            ReceiveMessage(handler);
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        public void ReceiveMessage(Socket handler)
        {
            try
            {
                StateObject state = new StateObject();
                state.workSocket = handler;
                // Begin receiving the data from the remote device.  
                handler.BeginReceive(state.buffer, 0, StateObject.bufferSize, SocketFlags.None, new AsyncCallback(ReceiveCallBack), state);
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
        }

        private void ReceiveCallBack(IAsyncResult ar)
        {
            try
            {
                StateObject state = (StateObject)ar.AsyncState;
                Socket handler = state.workSocket;
                int read = handler.EndReceive(ar);
                if (read > 0)
                {
                    Receive receive = _recevieDic[handler];
                    receive.ReceiveMessage(state.buffer);
                    ReceiveMessage(handler);
                }
                else
                {
                    handler.Close();
                }
            }
            catch (SocketException se)
            {
                Console.WriteLine(se.Message);
            }
        }

        /// <summary>
        /// 向客户端 Socket 发送数据
        /// </summary>
        /// <param name="message"></param>
        public void Send(int uid, int cmdID, string message)
        {
            foreach(var kv in _recevieDic)
            {
                Socket socket = kv.Key;
                byte[] byteData = Encoding.ASCII.GetBytes(message);
                Send(socket, uid, cmdID, byteData);
            }
        }

        public void Send(int uid, int cmdID, byte[] byteData)
        {
            foreach (var kv in _recevieDic)
            {
                Socket socket = kv.Key;
                Send(socket, uid, cmdID, byteData);
            }
        }

        private void Send(Socket handler, int uid, int cmdID, byte[] byteData)
        {
            byte[] uidBytes = BitConverter.GetBytes(uid);
            byte[] cmdBytes = BitConverter.GetBytes(cmdID);
            int length = uidBytes.Length + cmdBytes.Length + byteData.Length;  // uid + cmd + 内容
            byte[] lengthBytes = BitConverter.GetBytes(length);

            ByteBuffer byteBuffer = new ByteBuffer((lengthBytes.Length + length));
            byteBuffer.WriteInt(length);
            byteBuffer.WriteBytes(uidBytes);
            byteBuffer.WriteBytes(cmdBytes);
            byteBuffer.WriteBytes(byteData);
            byte[] sendBytes = byteBuffer.GetData();

            handler.BeginSend(sendBytes, 0, sendBytes.Length, SocketFlags.None, new AsyncCallback(SendCallBack), handler);
        }

        private void SendCallBack(IAsyncResult ar)
        {
            try
            {
                Socket handler = (Socket)ar.AsyncState;
                int bytesSend = handler.EndSend(ar);
                //handler.Shutdown(SocketShutdown.Both);
                //handler.Close();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }

        private void ReceiveComplete(int uid, int cmdID, byte[] byteData)
        {
            string content = Encoding.ASCII.GetString(byteData);
            Debug.Log("uid : " + uid + "    cmdID : " + cmdID + "   content : " + content);

            Send(uid, cmdID, content);
        }
    }

}
