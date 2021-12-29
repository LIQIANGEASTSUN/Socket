using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace Client
{

    class SocketClient
    {
        private Socket clientSocket = null;
        private string ip = "10.1.10.103";
        private int prot = 8000;

        private int uid = 10000;
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
                Debug.Log("连接服务器失败，请回车退出");
            }
        }

        // 连接服务器成功
        private void ConnectCallBack(IAsyncResult ar)
        {
            Debug.Log("连接服务器成功");
            try
            {
                StateObject stateObject = (StateObject)ar.AsyncState;
                Socket socket = stateObject.socket;
                socket.EndConnect(ar);

            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
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
                stateObject.socket.BeginReceive(stateObject.bytes, 0, stateObject.size, 0, new AsyncCallback(ReceiveCallBack), stateObject);
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
                StateObject stateObject = (StateObject)ar.AsyncState;
                Socket socket = stateObject.socket;
                int REnd = socket.EndReceive(ar);
                if (REnd > 0)
                {
                    Receive.ReceiveMessage(stateObject.bytes);
                    ReceiveMessage();
                }
            }
            catch(SocketException se)
            {
                Debug.Log(se.Message);
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="bytes"></param>
        public void SendMessage( int cmdID, byte[] bytes)
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
                stateObject.socket = clientSocket;
                byte[] byteData = byteBuffer.GetData();
                stateObject.socket.BeginSend(byteData, 0, byteData.Length, 0, SendCallBack, stateObject);
            }
            catch(Exception ex)
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
                StateObject stateObject = (StateObject)ar.AsyncState;
                Socket socket = stateObject.socket;
                socket.EndSend(ar);
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
                clientSocket.Dispose();//调用的上一个函数
                clientSocket.Close();
                clientSocket = null;
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }


    }
}