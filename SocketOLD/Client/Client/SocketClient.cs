using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Client
{

    class SocketClient
    {
        private Socket clientSocket = null;
        private string ip = "10.1.10.103";
        private int prot = 8000;

        public SocketClient()
        {
            
        }

        public void StartConnect()
        {
            IPAddress ipAddress = IPAddress.Parse(ip);
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, prot);
            clientSocket.SendTimeout = 20000;

            try
            {
                StateObject stateObject = new StateObject();
                stateObject.socket = clientSocket;
                stateObject.socket.BeginConnect(ipEndPoint, ConnectCallBack, stateObject);  // 异步连接
            }
            catch (Exception ex)
            {
                Console.WriteLine("连接服务器失败，请回车退出");
            }
        }

        // 连接服务器成功
        private void ConnectCallBack(IAsyncResult ar)
        {
            Console.WriteLine("连接服务器成功");
            try
            {
                StateObject stateObject = (StateObject)ar.AsyncState;
                Socket socket = stateObject.socket;
                socket.EndConnect(ar);

                SnedMessage("Hello Server I'm Client");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        public void ReceiveMessage()
        {
            try
            {
                StateObject stateObject = new StateObject();
                stateObject.socket = clientSocket;
                stateObject.socket.BeginReceive(stateObject.bytes, 0, stateObject.size, 0, new AsyncCallback(Receive), stateObject);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
        }

        private void Receive(IAsyncResult ar)
        {
            try
            {
                StateObject stateObject = (StateObject)ar.AsyncState;
                Socket socket = stateObject.socket;
                int REnd = socket.EndReceive(ar);
                if (REnd > 0)
                {
                    byte[] data = new byte[REnd];
                    Array.Copy(stateObject.bytes, data, REnd);

                    string content = Encoding.ASCII.GetString(data);
                    Console.WriteLine(content);

                    ReceiveMessage();
                }
            }
            catch(SocketException se)
            {
                Console.WriteLine(se.Message);
            }
        }
        
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message"></param>
        public void SnedMessage(string message)
        {
            SendMessage(Encoding.ASCII.GetBytes(message));
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="bytes"></param>
        private void SendMessage(byte[] bytes)
        {
            try
            {
                int length = bytes.Length;
                byte[] head = BitConverter.GetBytes(length);
                byte[] data = new byte[head.Length + bytes.Length];

                Array.Copy(head, data, head.Length);
                Array.Copy(bytes, 0, data, head.Length, bytes.Length);

                StateObject stateObject = new StateObject();
                stateObject.socket = clientSocket;
                stateObject.socket.BeginSend(data, 0, data.Length, 0, SendCallBack, stateObject);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                StateObject stateObject = (StateObject)ar.AsyncState;
                Socket socket = stateObject.socket;
                socket.EndSend(ar);
            }
            catch (SocketException se)
            {
                Console.WriteLine(se.Message);
            }
        }

        public void Dispose()
        {
            try
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Dispose();//调用的上一个函数
                clientSocket.Close();
                clientSocket = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


    }
}