using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;

namespace Network
{
    public class TcpReceive
    {
        private byte[] byteBuffer;
        private int _start = 0;
        private int _residue;

        private const int lengthBit = 4;
        private const int messageIdBit = 4;
        private const int seqIdBit = 4;

        private Action<int, int, byte[]> _callBack;
        public TcpReceive()
        {
            Init();
        }

        public void Clear()
        {
            Init();
        }

        private void Init()
        {
            _start = 0;
            _residue = 0;

            // 因为字节在 byteBuffer 是循环存储的
            // 确保 byteBuffer 缓存的数组长度 >= StateObject.bufferSize * 2
            // 否则会出现收尾相连
            byteBuffer = new byte[StateObject.bufferSize * 2];
        }

        public void SetCompleteCallBack(Action<int, int, byte[]> callBacka)
        {
            _callBack = callBacka;
        }

        public void ReceiveMessage(int count, byte[] bytesData)
        {
            WriteByte(count, bytesData);
            Analyse();
        }

        private void WriteByte(int count, byte[] bytesData)
        {
            int offset = _start + _residue;
            offset %= byteBuffer.Length;
            _residue += count;

            int index = 0;
            while (count > 0)
            {
                int write = 0;
                if (_start < offset)
                {
                    write = byteBuffer.Length - offset + _start;
                }
                else
                {
                    write = _start - offset;
                }
                write = Math.Min(write, count);

                Array.Copy(bytesData, index, byteBuffer, offset, write);

                index += write;
                count -= write;
                offset += write;
                offset %= byteBuffer.Length;
            }
        }

        private void Analyse()
        {
            while (_residue >= lengthBit)
            {
                byte[] lengthByte = CopyByte(_start, 4);
                int length = BitConverter.ToInt32(lengthByte, 0);

                if (_residue < length)
                {
                    break;
                }

                byte[] msgBytes = CopyByte(_start, length);
                CompleteBuff(msgBytes);

                _start += length;
                _start %= byteBuffer.Length;

                _residue -= length;
            }
        }

        private byte[] CopyByte(int start, int count)
        {
            byte[] bytes = new byte[count];

            int index = 0;
            while (count > 0)
            {
                if (start >= byteBuffer.Length)
                {
                    start = 0;
                }
                int copy = byteBuffer.Length - start;
                copy = Math.Min(copy, count);

                Array.Copy(byteBuffer, start, bytes, index, copy);

                count -= copy;
                index += copy;
                start += copy;
            }

            return bytes;
        }

        //public void ReceiveMessage(byte[] bytesData)
        //{
        //    int readIndex = 0;
        //    while (readIndex < bytesData.Length)
        //    {
        //        int readCount = 0;
        //        if (_offset < headBit)
        //        {
        //            readCount = headBit - _offset;
        //            if (readIndex + readCount > bytesData.Length)
        //            {
        //                readCount = bytesData.Length - readIndex;
        //                Array.Copy(bytesData, readIndex, byteBuffer, _offset, readCount);
        //                _offset += readCount;
        //                break;
        //            }
        //            Array.Copy(bytesData, readIndex, byteBuffer, _offset, headBit - _offset);
        //            readIndex += readCount;
        //            _offset += readCount;
        //        }
        //        int length = BitConverter.ToInt32(byteBuffer, 0);
        //        if (length <= 0)
        //        {
        //            _offset = 0;
        //            break;
        //        }
        //        readCount = length + headBit - _offset;
        //        Array.Copy(bytesData, readIndex, byteBuffer, _offset, readCount);
        //        readIndex += readCount;
        //        _offset += readCount;
        //        CompleteBuff(byteBuffer);
        //        _offset = 0;
        //    }
        //}

        private void CompleteBuff(byte[] bytes)
        {
            int length = BitConverter.ToInt32(bytes, 0);
            int messageId = BitConverter.ToInt32(bytes, lengthBit);
            int seqId = BitConverter.ToInt32(bytes, lengthBit + messageIdBit);

            byte[] byteData = new byte[length - lengthBit - messageIdBit - seqIdBit];
            Array.Copy(bytes, lengthBit + messageIdBit + seqIdBit, byteData, 0, byteData.Length);

            if (null != _callBack)
            {
                _callBack(messageId, seqId, byteData);
            }
        }

    }
}