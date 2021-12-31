﻿using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;

namespace Network
{
    public class Receive
    {
        private ByteBuffer byteBuffer;

        private int head = 0;
        private int cmd = 0;

        private Action<int, int, byte[]> _callBack;
        public Receive()
        {
            byteBuffer = new ByteBuffer(1024);
            head = 4;
            cmd = 4;
        }

        public void SetCompleteCallBack(Action<int, int, byte[]> callBacka)
        {
            _callBack = callBacka;
        }

        public void ReceiveMessage(byte[] bytes)
        {
            // 有之前的缓存数据
            if (byteBuffer.writeIndex > 0)
            {
                HasCache(bytes);
                return;
            }

            NoCache(bytes);
        }

        private void HasCache(byte[] bytes)
        {
            if (byteBuffer.writeIndex >= head)  // 可以读取到消息长度
            {
                byte[] byteData = byteBuffer.GetData();
                int headLength = BitConverter.ToInt32(byteData, 0);
                if (headLength <= 0)
                {
                    byteBuffer.Clear();
                    return;
                }

                // 是一个完整的消息了
                if (byteBuffer.writeIndex >= (headLength + head))
                {
                    int uid = BitConverter.ToInt32(byteData, head);
                    int cmdID = BitConverter.ToInt32(byteData, head + head);
                    byteData = new byte[headLength];
                    Array.Copy(byteBuffer.GetData(), (head + head + cmd), byteData, 0, headLength);

                    CompleteBuff(byteData);

                    byteBuffer.Clear();

                    ReceiveMessage(bytes);
                }
                else // 不是一个完整的消息
                {
                    int lastLentgh = headLength + head - byteBuffer.writeIndex;

                    if (bytes.Length >= lastLentgh)
                    {
                        byte[] copyBytes = new byte[lastLentgh];
                        Array.Copy(bytes, copyBytes, lastLentgh);

                        byteBuffer.WriteBytes(copyBytes);

                        CompleteBuff(byteBuffer.GetData());

                        byteBuffer.Clear();

                        byte[] leftBytes = new byte[bytes.Length - lastLentgh];
                        Array.Copy(bytes, lastLentgh, leftBytes, 0, bytes.Length - lastLentgh);

                        ReceiveMessage(leftBytes);
                    }
                    else
                    {
                        byteBuffer.WriteBytes(bytes);
                    }
                }
            }
            else  // 缓存数据不包含消息头
            {
                int writeIndex = byteBuffer.writeIndex;
                if (writeIndex < head)  // 缓存数据不大于消息头
                {
                    if (bytes.Length < (head - writeIndex))
                    {
                        byteBuffer.WriteBytes(bytes);
                        return;
                    }

                    byte[] headBuff = new byte[head - writeIndex];
                    Array.Copy(bytes, 0, headBuff, 0, head - writeIndex);

                    byteBuffer.WriteBytes(headBuff);

                    byte[] byteData = new byte[bytes.Length - (head - writeIndex)];

                    Array.Copy(bytes, head - writeIndex, byteData, 0, byteData.Length);
                    ReceiveMessage(byteData);
                }
            }
        }

        private void NoCache(byte[] bytes)
        {
            byteBuffer.Clear();

            if (bytes.Length < head)
            {
                byteBuffer.WriteBytes(bytes);
                return;
            }

            byte[] headBytes = new byte[head];
            Array.Copy(bytes, headBytes, head);

            byteBuffer.WriteBytes(headBytes);

            byte[] byteData = new byte[bytes.Length - head];
            Array.Copy(bytes, head, byteData, 0, byteData.Length);
            ReceiveMessage(byteData);
        }

        private void CompleteBuff(byte[] bytes)
        {
            int headLength = BitConverter.ToInt32(bytes, 0);
            int uid = BitConverter.ToInt32(bytes, head);
            int cmdID = BitConverter.ToInt32(bytes, head + cmd);

            byte[] byteData = new byte[bytes.Length - head - head - cmd];
            Array.Copy(bytes, head + head + cmd, byteData, 0, byteData.Length);

            if (null != _callBack)
            {
                _callBack(uid, cmdID, byteData);
            }
        }

    }
}