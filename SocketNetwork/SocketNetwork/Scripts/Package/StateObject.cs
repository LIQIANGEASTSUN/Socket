using System.Net;
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
        public EndPoint remote = new IPEndPoint(IPAddress.Any, 0);
    }

    public class NetworkConstant
    {
        public const int SIZE_BIT = 4;
        public const int UID_BIT = 4;
        public const int CMDID_BIT = 4;
        public const int QUEUEID_BIT = 4;

        public const int HEAD_BIT = 16;  // SIZE_BIT + UID_BIT + CMDID_BIT + QUEUEID_BIT
    }
}
