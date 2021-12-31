using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;

namespace Network
{
    public struct ByteBuffer
    {
        /// <summary>
        /// 字节缓存区
        /// </summary>
        private byte[] byteData;
        public int writeIndex;

        public ByteBuffer(int capacity)
        {
            byteData = new byte[capacity];
            writeIndex = 0;
        }

        public void WriteInt(int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            WriteBytes(bytes);
        }

        public void WriteString(string content)
        {
            byte[] bytes = GetBytes(content);
            WriteBytes(bytes);
        }

        public void WriteBytes(byte[] bytes)
        {
            Array.Copy(bytes, 0, byteData, writeIndex, bytes.Length);
            writeIndex += bytes.Length;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public byte[] GetData()
        {
            byte[] bytes = new byte[writeIndex];
            Array.Copy(byteData, 0, bytes, 0, writeIndex);
            return bytes;
        }

        /// <summary>
        /// 字符串转换为 byte 数组
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static byte[] GetBytes(string content)
        {
            return Encoding.ASCII.GetBytes(content);
        }

        public void Clear()
        {
            writeIndex = 0;
        }
    }
}