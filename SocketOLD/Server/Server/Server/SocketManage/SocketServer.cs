using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace Server
{
   
    class SocketServer
    {
        private static int prot = 8000;                  // 端口
        private Socket serverSocket = null;
        private List<Socket> clientSocketList = new List<Socket>();

        private string ip = "10.1.10.103";
        public SocketServer()
        {
            
        }

        public void Start()
        {
            IPAddress ipAddress = IPAddress.Parse(ip);    // 服务器 IP
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, prot);
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(ipEndPoint);
            serverSocket.Listen(100);

            Console.WriteLine("启动监听{0}成功", serverSocket.LocalEndPoint.ToString());

            ListenClientConnect();
        }

        private ManualResetEvent allDone = new ManualResetEvent(false);
        // 监听客户端连接
        private void ListenClientConnect()
        {
            try
            {
                while (true)
                {
                    allDone.Reset();
                    serverSocket.BeginAccept(new AsyncCallback(AcceptCallBack), serverSocket);
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

            Socket serverSocket = (Socket)ar.AsyncState;
            Socket clientSocket = serverSocket.EndAccept(ar);
            clientSocketList.Add(clientSocket);

            StateObject stateObject = new StateObject();
            stateObject.socket = clientSocket;
            clientSocket.BeginReceive(stateObject.bytes, 0, stateObject.size, 0, new AsyncCallback(ReceiveCallBack), stateObject);
        }

        private byte[] byteBuff = new byte[1024];
        private void ReceiveCallBack(IAsyncResult ar)
        {
            StateObject stateObject = (StateObject)ar.AsyncState;
            Socket clientSocket = stateObject.socket;
            try
            {
                int REnd = clientSocket.EndReceive(ar);
                if (REnd > 0)
                {
                    Receive.ReceiveMessage(stateObject.bytes);
                    clientSocket.BeginReceive(stateObject.bytes, 0, stateObject.size, 0, new AsyncCallback(ReceiveCallBack), stateObject);
                }
                else
                {
                    //Dispose();
                    //clientSocket.Close();
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
        public void SendMessage(int uid, int cmdID, string message)
        {
            for (int i = 0; i < clientSocketList.Count; ++i)
            {
                Socket socket = clientSocketList[i];
                SendMessage(socket, uid, cmdID, Encoding.ASCII.GetBytes(message));
            }
        }

        private void SendMessage(Socket handler, int uid, int cmdID, byte[] byteData)
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

            //StateObject stateObject = new StateObject();
            //stateObject.socket = handler;
            //byte[] byteData = byteBuffer.GetData();

            byte[] sendBytes = byteBuffer.GetData();

            handler.BeginSend(sendBytes, 0, sendBytes.Length, 0, new AsyncCallback(SendCallBack), handler);
        }

        private void Send(Socket handler, string message)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(message);
            handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallBack), handler);
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
    }
}
