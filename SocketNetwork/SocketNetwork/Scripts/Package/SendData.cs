﻿using System;
using System.Net;

namespace Network
{
    public class IPAddressTool
    {
        public static int HostToNetworkOrderInt32(int value)
        {
            if (BitConverter.IsLittleEndian)
            {
                return IPAddress.HostToNetworkOrder(value);
            }
            return value;
        }

        public static int HostToNetworkOrderInt64(int value)
        {
            if (BitConverter.IsLittleEndian)
            {
                return IPAddress.HostToNetworkOrder(value);
            }
            return value;
        }

        public static int NetworkToHostOrderInt32(int value)
        {
            if (BitConverter.IsLittleEndian)
            {
                return IPAddress.NetworkToHostOrder(value);
            }
            return value;
        }

        public static Int64 NetworkToHostOrderInt64(Int64 value)
        {
            if (BitConverter.IsLittleEndian)
            {
                return IPAddress.NetworkToHostOrder(value);
            }
            return value;
        }

        public static byte[] HostToNetworkOrderByte(int value)
        {
            value = HostToNetworkOrderInt32(value);
            return BitConverter.GetBytes(value);
        }

        public static int NetworkToHostOrderInt32(byte[] byteData)
        {
            int value = BitConverter.ToInt32(byteData, 0);
            return NetworkToHostOrderInt32(value);
        }

        public static Int64 NetworkToHostOrderInt64(byte[] byteData)
        {
            Int64 value = BitConverter.ToInt64(byteData, 0);
            return NetworkToHostOrderInt64(value);
        }
    }

    class SendData
    {
        #region Tcp

        /// <summary>
        /// 发送的Tcp消息长度 length = SIZE_BIT + uid字节长度 + cmdID 字节长度 + queueId字节长度 + bytesData字节长度
        /// 发送消息内容为：length字节 + uid字节 + cmdID字节 + queueId字节 + bytesData字节
        /// </summary>
        /// <param name="uid">玩家id</param>
        /// <param name="cmdId">消息号id</param>
        /// <param name="queueId">消息序号</param>
        /// <param name="bytesData"></param>
        /// <returns></returns>
        public static byte[] ToTcpByte(int uid, int cmdId, int queueId, byte[] bytesData)
        {
            // // SIZE_BIT + UID_BIT + CMDID_BIT + QUEUEID_BIT + sendBytes
            byte[] uidBytes = IPAddressTool.HostToNetworkOrderByte(uid);
            byte[] cmdIdBytes = IPAddressTool.HostToNetworkOrderByte(cmdId);
            byte[] queueIdBytes = IPAddressTool.HostToNetworkOrderByte(queueId);

            int size = NetworkConstant.TCP_HEAD_BIT + bytesData.Length;
            byte[] sizeBytes = IPAddressTool.HostToNetworkOrderByte(size);

            byte[] sendBytes = new byte[size];
            long index = 0;
            ByteTool.CopyBytes(sizeBytes, sendBytes, NetworkConstant.SIZE_BIT, ref index);
            ByteTool.CopyBytes(uidBytes, sendBytes, NetworkConstant.UID_BIT, ref index);
            ByteTool.CopyBytes(cmdIdBytes, sendBytes, NetworkConstant.CMDID_BIT, ref index);
            ByteTool.CopyBytes(queueIdBytes, sendBytes, NetworkConstant.QUEUEID_BIT, ref index);
            ByteTool.CopyBytes(bytesData, sendBytes, bytesData.Length, ref index);

            return sendBytes;
        }
        #endregion

        #region UDP
        public static byte[] ToUdpByte(int uid, int cmdId, int queueId, byte[] bytesData)
        {
            // SIZE_BIT + UID_BIT + CMDID_BIT + QUEUEID_BIT + PACKAGE_COUNT_BIT + PACKAGE_INDEX_BIT
            byte[] uidBytes = IPAddressTool.HostToNetworkOrderByte(uid);
            byte[] cmdIdBytes = IPAddressTool.HostToNetworkOrderByte(cmdId);
            byte[] queueIdBytes = IPAddressTool.HostToNetworkOrderByte(queueId);
            int packageCount = 1; // 目前一个消息一个包，不用分包
            int pcakageIndex = 0; // 只有一个包，所以index = 0

            byte[] packageCountBytes = IPAddressTool.HostToNetworkOrderByte(packageCount);
            byte[] pcakageIndexBytes = IPAddressTool.HostToNetworkOrderByte(pcakageIndex);

            int size = NetworkConstant.UDP_HEAD_BIT + bytesData.Length;
            byte[] sizeBytes = IPAddressTool.HostToNetworkOrderByte(size);

            byte[] sendBytes = new byte[size];
            long index = 0;
            ByteTool.CopyBytes(sizeBytes, sendBytes, NetworkConstant.SIZE_BIT, ref index);
            ByteTool.CopyBytes(uidBytes, sendBytes, NetworkConstant.UID_BIT, ref index);
            ByteTool.CopyBytes(cmdIdBytes, sendBytes, NetworkConstant.CMDID_BIT, ref index);
            ByteTool.CopyBytes(queueIdBytes, sendBytes, NetworkConstant.QUEUEID_BIT, ref index);
            ByteTool.CopyBytes(packageCountBytes, sendBytes, NetworkConstant.PACKAGE_COUNT_BIT, ref index);
            ByteTool.CopyBytes(pcakageIndexBytes, sendBytes, NetworkConstant.PACKAGE_INDEX_BIT, ref index);
            ByteTool.CopyBytes(bytesData, sendBytes, bytesData.Length, ref index);
            return sendBytes;
        }
        #endregion
    }
}