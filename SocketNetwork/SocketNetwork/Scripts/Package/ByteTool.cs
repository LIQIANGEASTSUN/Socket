using System;
using System.Collections.Generic;


namespace Network
{
    public class ByteTool
    {

        /// <summary>
        /// 从 sourceArray 第 index 位置开始读取 count 个字节
        /// </summary>
        /// <param name="sourceArray"></param>
        /// <param name="index">开启读取的字节位置</param>
        /// <param name="count">读取个数</param>
        /// <param name="newIndex">等于 index + count</param>
        /// <returns></returns>
        public static byte[] ReadBytes(Array sourceArray, int index, int count, ref int newIndex)
        {
            byte[] bytes = new byte[count];
            Array.Copy(sourceArray, index, bytes, 0, count);
            newIndex = index + count;
            return bytes;
        }

        /// <summary>
        /// 将 sourceArray 中从第 0 个字节，拷贝 length 个字节 到 destinationArray，
        /// 从 destinationArray 的 destinationIndex 字节开始
        /// </summary>
        /// <param name="sourceArray">从这里拷贝</param>
        /// <param name="destinationArray">拷贝到这里</param>
        /// <param name="length">拷贝个数</param>
        /// <param name="destinationIndex">等于 destinationIndex + length</param>
        public static void CopyBytes(Array sourceArray, Array destinationArray, long length, ref long destinationIndex)
        {
            Array.Copy(sourceArray, 0, destinationArray, destinationIndex, length);
            destinationIndex += length;
        }

    }
}