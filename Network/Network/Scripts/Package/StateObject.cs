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
        public Socket socket;
        public byte[] bytes = new byte[1024];
        public int size = 1024;
    }
}
