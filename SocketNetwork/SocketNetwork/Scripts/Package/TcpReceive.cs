using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;

namespace Network
{
    public class TcpReceive
    {
        private byte[] byteBuffer;
        private int _offset = 0;

        private const int headBit = 4;
        private const int uidBit = 4;
        private const int cmdBit = 4;

        private Action<int, int, byte[]> _callBack;
        public TcpReceive()
        {
            byteBuffer = new byte[StateObject.bufferSize];
        }

        public void Clear()
        {
            byteBuffer = new byte[StateObject.bufferSize];
            _offset = 0;
        }

        public void SetCompleteCallBack(Action<int, int, byte[]> callBacka)
        {
            _callBack = callBacka;
        }

        public void ReceiveMessage(byte[] bytesData)
        {
            int readIndex = 0;
            while (readIndex < bytesData.Length)
            {
                int readCount = 0;
                if (_offset < headBit)
                {
                    readCount = headBit - _offset;
                    if (readIndex + readCount > bytesData.Length)
                    {
                        readCount = bytesData.Length - readIndex;
                        Array.Copy(bytesData, readIndex, byteBuffer, _offset, readCount);
                        _offset += readCount;
                        break;
                    }
                    Array.Copy(bytesData, readIndex, byteBuffer, _offset, headBit - _offset);
                    readIndex += readCount;
                    _offset += readCount;
                }
                int length = BitConverter.ToInt32(byteBuffer, 0);
                if (length <= 0)
                {
                    _offset = 0;
                    break;
                }
                readCount = length + headBit - _offset;
                Array.Copy(bytesData, readIndex, byteBuffer, _offset, readCount);
                readIndex += readCount;
                _offset += readCount;
                CompleteBuff(byteBuffer);
                _offset = 0;
            }
        }

        private void CompleteBuff(byte[] bytes)
        {
            int headLength = BitConverter.ToInt32(bytes, 0);
            int uid = BitConverter.ToInt32(bytes, headBit);
            int cmdID = BitConverter.ToInt32(bytes, headBit + uidBit);

            byte[] byteData = new byte[headLength - uidBit - cmdBit];
            Array.Copy(bytes, headBit + uidBit + cmdBit, byteData, 0, byteData.Length);

            if (null != _callBack)
            {
                _callBack(uid, cmdID, byteData);
            }
        }

    }
}