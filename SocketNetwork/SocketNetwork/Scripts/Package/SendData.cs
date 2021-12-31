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

            ByteBuffer byteBuffer = new ByteBuffer((lengthBytes.Length + length));
            byteBuffer.WriteInt(length);
            byteBuffer.WriteBytes(uidBytes);
            byteBuffer.WriteBytes(cmdBytes);
            byteBuffer.WriteBytes(bytesData);

            byte[] byteData = byteBuffer.GetData();
            return byteData;
        }

    }
}
