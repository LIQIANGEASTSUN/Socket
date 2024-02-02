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

            Analyse();
        }

        private void Analyse()
        {
            int index = 0;
            byte[] sizeByte = CopyByte(index, NetworkConstant.SIZE_BIT, ref index);
            int size = IPAddressTool.NetworkToHostOrderInt32(sizeByte);

            if (_byteCount < size)
            {
                return;
            }

            byte[] uidByte = CopyByte(index, NetworkConstant.UID_BIT, ref index);
            byte[] cmdIdByte = CopyByte(index, NetworkConstant.CMDID_BIT, ref index);
            byte[] queueIdByte = CopyByte(index, NetworkConstant.QUEUEID_BIT, ref index);
            byte[] msgBytes = CopyByte(index, size - NetworkConstant.HEAD_BIT, ref index);
            CompleteBuff(uidByte, cmdIdByte, queueIdByte, msgBytes);

            _byteCount -= size;
            if (_byteCount > 0)
            {
                // 将剩余的字节往前挪到字节数组0位置开始
                Array.Copy(byteBuffer, index, byteBuffer, 0, _byteCount);
            }
        }

        private byte[] CopyByte(int index, int count, ref int newIndex)
        {
            byte[] bytes = new byte[count];
            Array.Copy(byteBuffer, index, bytes, 0, count);
            newIndex = index + count;
            return bytes;
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