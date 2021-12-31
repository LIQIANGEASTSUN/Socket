using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    class UdpReceive
    {
        private byte[] byteBuffer;
        private int _offset = 0;

        private const int headBit = 4;
        private const int uidBit = 4;
        private const int cmdBit = 4;

        private Action<int, int, byte[]> _callBack;
        public UdpReceive()
        {
            byteBuffer = new byte[StateObject.bufferSize];
        }

        public void SetCompleteCallBack(Action<int, int, byte[]> callBacka)
        {
            _callBack = callBacka;
        }

        public void ReceiveMessage(byte[] bytesData)
        {
            CompleteBuff(bytesData);
        }

        private void CompleteBuff(byte[] bytes)
        {
            int headLength = BitConverter.ToInt32(bytes, 0);
            int uid = BitConverter.ToInt32(bytes, headBit);
            int cmdID = BitConverter.ToInt32(bytes, headBit + uidBit);

            byte[] byteData = new byte[headLength - uidBit - cmdBit];
            Array.Copy(bytes, headBit + uidBit + cmdBit, byteData, 0, byteData.Length);
            string content = Encoding.ASCII.GetString(byteData);

            if (null != _callBack)
            {
                _callBack(uid, cmdID, byteData);
            }
        }

    }
}