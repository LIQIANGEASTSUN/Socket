using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Client;
using System;
using System.Text;

public class SocketManage  {

    private SocketClient socketClient;
    public SocketManage()
    {
        socketClient = new SocketClient();

        socketClient.StartConnect();

        socketClient.ReceiveMessage();
    }

    public void SnedMessage(int cmdID, string content)
    {
        socketClient.SendMessage(cmdID, Encoding.ASCII.GetBytes(content));
    }
}
