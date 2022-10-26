using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    class SendData
    {
        #region Tcp
        public static byte[] ToTcpByte(int uid, int cmdID, string msg)
        {
            byte[] bytes = Encoding.Default.GetBytes(msg);
            return ToTcpByte(uid, cmdID, bytes);
        }

        /// <summary>
        /// 发送的Tcp消息
        /// length = uid字节长度 + cmdID 字节长度 + bytesData字节长度
        /// 发送消息内容为：length + uid + cmdID + bytesData
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="cmdID"></param>
        /// <param name="bytesData"></param>
        /// <returns></returns>
        public static byte[] ToTcpByte(int messageId, int seqId, byte[] bytesData)
        {
            byte[] msgIdBytes = BitConverter.GetBytes(messageId);
            byte[] seqIdBytes = BitConverter.GetBytes(seqId);

            int length = msgIdBytes.Length + seqIdBytes.Length + bytesData.Length;  // uid + cmd + 内容
            length += 4;
            byte[] lengthBytes = BitConverter.GetBytes(length);

            byte[] sendBytes = new byte[length];
            long index = 0;
            Copy(lengthBytes, sendBytes, lengthBytes.Length, ref index);
            Copy(msgIdBytes, sendBytes, msgIdBytes.Length, ref index);
            Copy(seqIdBytes, sendBytes, seqIdBytes.Length, ref index);
            Copy(bytesData, sendBytes, bytesData.Length, ref index);

            return sendBytes;
        }
        #endregion

        private static void Copy(Array sourceArray, Array destinationArray, long length, ref long destinationIndex)
        {
            Copy(sourceArray, 0, destinationArray, length, ref destinationIndex);
        }

        private static void Copy(Array sourceArray, int sourceIndex, Array destinationArray, long length, ref long destinationIndex)
        {
            Array.Copy(sourceArray, sourceIndex, destinationArray, destinationIndex, length);
            destinationIndex += length;
        }

    }
}
