using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public static class SocketManage
    {
        private static SocketServer socketServer;

        static SocketManage()
        {
        }

        public static void Init()
        {
            socketServer = new SocketServer();
            socketServer.Start();
            int a = 0;
        }

        public static void SendMessage(int uid, int cmdID, string message)
        {
            socketServer.SendMessage(uid, cmdID, message);
        }
    }
}
