﻿using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class InputManager : MonoBehaviour {

	SerialPort sp = new SerialPort("COM6", 9600);
	// Use this for initialization

	void Start () {
		sp.Open ();
		sp.ReadTimeout = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (sp.IsOpen) {
			try {
				Debug.Log(sp.ReadByte ());
			} catch (System.Exception) {
			}
		}
	}
}
	/*
	//SerialPort serial1;
	byte[] buf = new byte[4]; // creates a byte array the size of the data you want to receive.
	int bufCount = 0;
	int a,b;

	void Start() {
		//serial1=new SerialPort();
	}

	 void OnGUI()
    {
        if(GUI.Button(new Rect(10,10,100,50),"read"))
        {
            //serial1.PortName="COM1";
            serial1.Parity=Parity.None;
            //serial1.BaudRate=9600;
            serial1.DataBits=8;
            serial1.StopBits=StopBits.One;
            serial1.Open();
            bufCount = 0;
            bufCount += serial1.Read(buf, bufCount, buf.Length - bufCount);
 
            a = 0;
            b = 0;
            while (a < bufCount)
            {
                b += buf[a];
                a++;
            }
            print(b);
            serial1.Close();
           
           
        }
	
	}
}*/
