using System;
using System.Collections.Generic;

namespace Network
{
    public class UdpReceive
    {
        private Action<int, int, int, byte[]> _callBack;
        public UdpReceive()
        {
        }

        public void SetCompleteCallBack(Action<int, int, int, byte[]> callBacka)
        {
            _callBack = callBacka;
        }

        public void ReceiveMessage(byte[] bytesData)
        {
            // SIZE_BIT + UID_BIT + CMDID_BIT + QUEUEID_BIT + PACKAGE_COUNT_BIT + PACKAGE_INDEX_BIT
            int index = 0;
            byte[] sizeByte = ByteTool.ReadBytes(bytesData, index, NetworkConstant.SIZE_BIT, ref index);
            int size = IPAddressTool.NetworkToHostOrderInt32(sizeByte);

            byte[] uidByte = ByteTool.ReadBytes(bytesData, index, NetworkConstant.UID_BIT, ref index);
            byte[] cmdIdByte = ByteTool.ReadBytes(bytesData, index, NetworkConstant.CMDID_BIT, ref index);
            byte[] queueIdByte = ByteTool.ReadBytes(bytesData, index, NetworkConstant.QUEUEID_BIT, ref index);
            byte[] packageCountByte = ByteTool.ReadBytes(bytesData, index, NetworkConstant.PACKAGE_COUNT_BIT, ref index);
            byte[] packageIndexByte = ByteTool.ReadBytes(bytesData, index, NetworkConstant.PACKAGE_INDEX_BIT, ref index);

            byte[] msgBytes = ByteTool.ReadBytes(bytesData, index, size - NetworkConstant.UDP_HEAD_BIT, ref index);
            CompleteBuff(uidByte, cmdIdByte, queueIdByte, packageCountByte, packageIndexByte, msgBytes);
        }

        private void CompleteBuff(byte[] uidByte, byte[] cmdIdByte, byte[] queueIdByte, byte[] packageCountByte, byte[] packageIndexByte, byte[] msgBytes)
        {
            int uid = IPAddressTool.NetworkToHostOrderInt32(uidByte);
            int cmdId = IPAddressTool.NetworkToHostOrderInt32(cmdIdByte);
            int queueId = IPAddressTool.NetworkToHostOrderInt32(queueIdByte);
            int packageCount = IPAddressTool.NetworkToHostOrderInt32(packageCountByte);
            int pcakageIndex = IPAddressTool.NetworkToHostOrderInt32(packageIndexByte);

            if (null != _callBack)
            {
                _callBack(uid, cmdId, queueId, msgBytes);
            }
        }

    }

    public class UdpReceiveDataController
    {
        private List<ReceiveData> receiveList = new List<ReceiveData>();

        public void Receive(int uid, int cmdID, int ququeId, int packageCount, int pcakageIndex, byte[] dataBytes)
        {
            if (receiveList.Count <= 0)
            {

            }
        }

    }

    public class ReceiveData
    {
        public int _queueId;
        public int _uid;
        public int _cmdId;
        public int _packageCount;
        public Dictionary<int, byte[]> _dataDic = new Dictionary<int, byte[]>();

        public ReceiveData(int messageNumber, int uid, int cmdId, int packageCount)
        {
            _queueId = messageNumber;
            _uid = uid;
            _cmdId = cmdId;
        }

        public bool Add(int index, byte[] data)
        {
            if (_dataDic.ContainsKey(index))
            {
                return false;
            }
            _dataDic[index] = data;
            return _dataDic.Count == _packageCount;
        }
    }

}