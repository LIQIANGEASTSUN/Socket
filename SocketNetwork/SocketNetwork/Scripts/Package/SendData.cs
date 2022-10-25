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
            return ToTcpByte( uid, cmdID, bytes);
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
        public static byte[] ToTcpByte(int uid, int cmdID, byte[] bytesData)
        {
            byte[] uidBytes = BitConverter.GetBytes(uid);
            byte[] cmdBytes = BitConverter.GetBytes(cmdID);
            int length = uidBytes.Length + cmdBytes.Length + bytesData.Length;  // uid + cmd + 内容
            byte[] lengthBytes = BitConverter.GetBytes(length);

            byte[] sendBytes = new byte[length + 4];
            long index = 0;
            Copy(lengthBytes, sendBytes, lengthBytes.Length, ref index);
            Copy(uidBytes, sendBytes, uidBytes.Length, ref index);
            Copy(cmdBytes, sendBytes, cmdBytes.Length, ref index);
            Copy(bytesData, sendBytes, bytesData.Length, ref index);

            return sendBytes;
        }
        #endregion

        #region Udp
        private static int _messageNumber;
        private static object _lockObj = new object();
        public static byte[] ToUdpByte(int uid, int cmdID, string msg)
        {
            byte[] bytes = Encoding.Default.GetBytes(msg);
            return ToTcpByte(uid, cmdID, bytes);
        }

        /// <summary>
        /// 发送Udp消息
        /// 每次发送的消息体最大字节长度为 StateObject.bufferSize
        /// 消息头：消息ID + 包总个数 + 当前包index + uid + cmd
        /// 每次发送的消息：消息头 + bytesData
        /// 如果每次发送消息的字节长度 > StateObject.bufferSize
        /// 则需要将消息分多次发送
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="cmdID"></param>
        /// <param name="bytesData"></param>
        /// <returns></returns>
        public static void ToUdpByte(int uid, int cmdID, string ip, int port, byte[] bytesData, Action<string, int, byte[]> callBack)
        {
            // 消息ID
            int messageNumber = NewMessageNumber;
            byte[] numberBytes = BitConverter.GetBytes(messageNumber);
            byte[] uidBytes = BitConverter.GetBytes(uid);
            byte[] cmdBytes = BitConverter.GetBytes(cmdID);

            // 每个包最大字节数，多于最大字节数需要将一个包分成多个子包，每个包按顺序排序
            int bufferSize = StateObject.bufferSize;

            // 消息头字节数：5 个int 的字节长度
            int headCount = numberBytes.Length * 6; // messageNumber + packageCount + pcakageIndex + bytesLength + uid + cmdID
            // 每次发送的最大字节数为：消息总字节数 - 消息头字节数
            int sendMaxSize = StateObject.bufferSize - headCount;
            int currentSize = 0;

            int packageCount = bytesData.Length / sendMaxSize + ((bytesData.Length % sendMaxSize) > 0 ? 1 : 0);
            byte[] packageCountBytes = BitConverter.GetBytes(packageCount);
            int pcakageIndex = 0;

            while (currentSize < bytesData.Length)
            {
                int enableSendDataLength = Math.Min(bufferSize - headCount, bytesData.Length - currentSize);
                if (enableSendDataLength <= 0)
                {
                    break;
                }

                // 发送的消息字节数
                int sendLength = headCount + enableSendDataLength;
                byte[] sendBytes = new byte[sendLength];

                long index = 0;
                Copy(numberBytes, sendBytes, numberBytes.Length, ref index);

                Copy(packageCountBytes, sendBytes, packageCountBytes.Length, ref index);

                byte[] packageIndexBytes = BitConverter.GetBytes(pcakageIndex);
                pcakageIndex++;
                Copy(packageIndexBytes, sendBytes, packageIndexBytes.Length, ref index);

                byte[] bytesLengthBytes = BitConverter.GetBytes(enableSendDataLength);
                Copy(bytesLengthBytes, sendBytes, bytesLengthBytes.Length, ref index);

                Copy(uidBytes, sendBytes, uidBytes.Length, ref index);

                Copy(cmdBytes, sendBytes, cmdBytes.Length, ref index);

                Copy(bytesData, currentSize, sendBytes, enableSendDataLength, ref index);

                currentSize += enableSendDataLength;

                callBack(ip, port, sendBytes);
            }
        }

        public static int NewMessageNumber
        {
            get
            {
                lock(_lockObj)
                {
                    ++_messageNumber;
                }
                return _messageNumber;
            }
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
