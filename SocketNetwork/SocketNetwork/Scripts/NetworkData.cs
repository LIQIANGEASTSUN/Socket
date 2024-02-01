
namespace Network
{
    public enum NetworkType
    {
        Tcp,
        Udp,
    }

    public enum NetworkCS
    {
        Client,
        Server,
    }

    public class NetworkData
    {
        public NetworkType networkType;
        public NetworkCS networkCS;
        public string localIp;
        public int localPort;
        public string remoteIp;
        public int remotePort;
        public int uid;       // 玩家ID
        public int cmdID;     // 消息号
        public int queueId;   // 消息序号，记录发送的第几条
    }
}
