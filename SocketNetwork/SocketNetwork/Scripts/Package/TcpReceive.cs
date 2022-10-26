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
        private int _end = 0;

        private const int lengthBit = 4;
        private const int messageIdBit = 4;
        private const int seqIdBit = 4;

        private Action<int, int, byte[]> _callBack;
        public TcpReceive()
        {
            byteBuffer = new byte[StateObject.bufferSize];
        }

        public void Clear()
        {
            byteBuffer = new byte[StateObject.bufferSize];
            _start = 0;
            _end = 0;
            _residue = 0;
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
                if (byteBuffer.Length == offset)
                {
                    offset = 0;
                }

                int write = Math.Max(0, byteBuffer.Length - offset);
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

        //public void ReceiveMessage(int count, byte[] bytesData)
        //{
        //    int end = _start + count;
        //    _end = end % byteBuffer.Length;

        //    if (end < byteBuffer.Length)
        //    {
        //        Array.Copy(bytesData, 0, byteBuffer, _start, count);
        //    }
        //    else
        //    {
        //        int oneCopyCount = count - _start;
        //        Array.Copy(bytesData, 0, byteBuffer, _start, oneCopyCount);

        //        int twoCopyCount = count - oneCopyCount;
        //        Array.Copy(bytesData, oneCopyCount, byteBuffer, 0, twoCopyCount);
        //    }

        //    int byteCount = ByteCount();
        //    while (byteCount >= lengthBit)
        //    {
        //        byte[] lengthByte = CopyByte(_start, 4);
        //        int length = BitConverter.ToInt32(lengthByte, 0);

        //        if (byteCount < length)
        //        {
        //            break;
        //        }

        //        byte[] msgBytes = CopyByte(_start, length);
        //        CompleteBuff(msgBytes);

        //        _start += length;
        //        _start %= byteBuffer.Length;

        //        byteCount = ByteCount();
        //    }

        //}

        //private int ByteCount()
        //{
        //    int count = 0;
        //    if (_end == _start)
        //    {
        //        return count;
        //    }
        //    if (_end > _start)
        //    {
        //        count = _end - _start;
        //    }
        //    else
        //    {
        //        count = byteBuffer.Length - _start + _end;
        //    }
        //    return count;
        //}

        //private byte[] CopyByte(int start, int count)
        //{
        //    byte[] bytes = new byte[count];

        //    if (byteBuffer.Length > start + count)
        //    {
        //        Array.Copy(byteBuffer, start, bytes, 0, count);
        //    }
        //    else
        //    {
        //        int oneCopyCount = byteBuffer.Length - start;
        //        Array.Copy(byteBuffer, start, bytes, 0, oneCopyCount);

        //        int twoCopyCount = count = oneCopyCount;
        //        Array.Copy(byteBuffer, 0, bytes, oneCopyCount, twoCopyCount);
        //    }

        //    return bytes;
        //}


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

        //private void CompleteBuff(byte[] bytes)
        //{
        //    int headLength = BitConverter.ToInt32(bytes, 0);
        //    int uid = BitConverter.ToInt32(bytes, headBit);
        //    int cmdID = BitConverter.ToInt32(bytes, headBit + uidBit);

        //    byte[] byteData = new byte[headLength - uidBit - cmdBit];
        //    Array.Copy(bytes, headBit + uidBit + cmdBit, byteData, 0, byteData.Length);

        //    if (null != _callBack)
        //    {
        //        _callBack(uid, cmdID, byteData);
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