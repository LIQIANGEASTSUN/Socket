﻿using System.Net;
using System.Net.Sockets;

namespace Network
{
    public class StateObject
    {
        public Socket workSocket;
        public const int bufferSize = 1024 * 5;
        public byte[] buffer = new byte[bufferSize];
    }

    public class StateUdpObject : StateObject
    {
        public EndPoint remote;
        public StateUdpObject(string ip, int port)
        {
            IPAddress ipAddress = IPAddress.Parse(ip);
            remote = new IPEndPoint(ipAddress, port);
        }
        //public EndPoint remote = new IPEndPoint(IPAddress.Any, 0);
    }

    public class NetworkConstant
    {
        public const int SIZE_BIT = 4;
        public const int UID_BIT = 4;
        public const int CMDID_BIT = 4;
        public const int QUEUEID_BIT = 4;

        public const int TCP_HEAD_BIT = 16;  // SIZE_BIT + UID_BIT + CMDID_BIT + QUEUEID_BIT

        public const int PACKAGE_COUNT_BIT = 4;
        public const int PACKAGE_INDEX_BIT = 4;
        public const int UDP_HEAD_BIT = 24;  // SIZE_BIT + UID_BIT + CMDID_BIT + QUEUEID_BIT + PACKAGE_COUNT_BIT + PACKAGE_INDEX_BIT 
    }
}
