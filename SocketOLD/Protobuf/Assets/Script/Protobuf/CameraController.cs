using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target = null;

    public float offsetX = 0;
    public float offsetY = 10;
    public float offsetZ = -16;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Controller();
	}

    private Vector3 targetPos = new Vector3();
    private float offsetRange = 1;
    private float speed = 1;
    private void Controller()
    {
        if (target == null)
        {
            return;
        }

        targetPos = target.position + new Vector3(offsetX, offsetY, offsetZ);

        float distance = Vector3.Distance(targetPos, transform.position);
        if (distance <= offsetRange)
        {
            return;
        }

        Vector3 offsetPos = targetPos - transform.position;
        offsetPos = offsetPos.normalized;

        //transform.Translate(offsetPos * speed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);
    }

}
