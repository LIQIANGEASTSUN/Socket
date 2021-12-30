using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Network
{
    class StateObject
    {
        public Socket workSocket;
        public const int bufferSize = 1024;
        public byte[] buffer = new byte[bufferSize];
    }
}
