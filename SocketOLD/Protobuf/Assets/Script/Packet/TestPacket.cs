using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPacket : MonoBehaviour {


    private SocketManage socketManage;
	// Use this for initialization
	void Start () {
        socketManage = new SocketManage();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.D))
        {
            socketManage.SnedMessage(100, "1234567890+-*/");
        }
	}
}
