using System;

namespace Network
{
    public class TcpReceive
    {
        private byte[] byteBuffer;
        private int _byteCount = 0;

        private Action<int, int, int, byte[]> _callBack;
        public TcpReceive()
        {
            Init();
        }

        private void Init()
        {
            byteBuffer = new byte[StateObject.bufferSize * 2];
        }

        public void Clear()
        {
            _byteCount = 0;
        }

        public void SetCompleteCallBack(Action<int, int, int, byte[]> callBack)
        {
            _callBack = callBack;
        }

        public void ReceiveMessage(int count, byte[] bytesData)
        {
            Array.Copy(bytesData, 0, byteBuffer, _byteCount, count);
            _byteCount += count;

            while (true)
            {
                bool result = Analyse();
                if (!result)
                {
                    break;
                }
            }
        }

        private bool Analyse()
        {
            int index = 0;
            byte[] sizeByte = ByteTool.ReadBytes(byteBuffer, index, NetworkConstant.SIZE_BIT, ref index);
            int size = IPAddressTool.NetworkToHostOrderInt32(sizeByte);

            if (_byteCount < size)
            {
                return false;
            }

            byte[] uidByte = ByteTool.ReadBytes(byteBuffer, index, NetworkConstant.UID_BIT, ref index);
            byte[] cmdIdByte = ByteTool.ReadBytes(byteBuffer, index, NetworkConstant.CMDID_BIT, ref index);
            byte[] queueIdByte = ByteTool.ReadBytes(byteBuffer, index, NetworkConstant.QUEUEID_BIT, ref index);
            byte[] msgBytes = ByteTool.ReadBytes(byteBuffer, index, size - NetworkConstant.TCP_HEAD_BIT, ref index);
            CompleteBuff(uidByte, cmdIdByte, queueIdByte, msgBytes);

            _byteCount -= size;
            if (_byteCount > 0)
            {
                // 将剩余的字节往前挪到字节数组0位置开始
                Array.Copy(byteBuffer, index, byteBuffer, 0, _byteCount);
            }
            return true;
        }

        private void CompleteBuff(byte[] uidByte, byte[] cmdIdByte, byte[] queueIdByte, byte[] msgBytes)
        {
            int uid = IPAddressTool.NetworkToHostOrderInt32(uidByte);
            int cmdId = IPAddressTool.NetworkToHostOrderInt32(cmdIdByte);
            int queueId = IPAddressTool.NetworkToHostOrderInt32(queueIdByte);
            if (null != _callBack)
            {
                _callBack(uid, cmdId, queueId, msgBytes);
            }
        }
    }
}