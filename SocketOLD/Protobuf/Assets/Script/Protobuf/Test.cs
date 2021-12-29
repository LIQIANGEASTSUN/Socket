using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;
using google.protobuf;
using System;
using System.IO;
using System.Text;


public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {

        DescriptorProto d = new DescriptorProto();

        d.name = "shaxa";

        DescriptorProto.ExtensionRange ext = new DescriptorProto.ExtensionRange();
        ext.start = 1;
        ext.end = 2;
        d.extension_range.Add(ext);

        MemoryStream ms = new MemoryStream();
        Serializer.Serialize<DescriptorProto>(ms, d);
        byte[] data = ms.ToArray();


        Debug.LogError("Data : " + data.ToString());

        Debug.LogError("-------------------------");

        MemoryStream ms1 = new MemoryStream(data);
        DescriptorProto descriptorProto = Serializer.Deserialize<DescriptorProto>(ms1);

        Debug.LogError("name : " + descriptorProto.name);
        for (int i = 0; i < descriptorProto.extension_range.Count; ++i)
        {
            DescriptorProto.ExtensionRange exten = descriptorProto.extension_range[i];
            Debug.LogError("Start : " + exten.start);
            Debug.LogError("end   : " + exten.end);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
