using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    class SendData
    {
        public static byte[] ToByte(int uid, int cmdID, string msg)
        {
            byte[] bytes = Encoding.Default.GetBytes(msg);
            return ToByte( uid, cmdID, bytes);
        }

        public static byte[] ToByte(int uid, int cmdID, byte[] bytesData)
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

        private static void Copy(Array sourceArray, Array destinationArray, long length, ref long destinationIndex)
        {
            Array.Copy(sourceArray, 0, destinationArray, destinationIndex, length);
            destinationIndex += length;
        }

    }
}
