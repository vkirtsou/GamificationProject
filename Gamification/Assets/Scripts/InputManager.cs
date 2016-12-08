using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;

public class InputManager : MonoBehaviour {

	[HideInInspector] public List<KeyCode> acceptedInputKeys = new List<KeyCode> {KeyCode.Keypad1, KeyCode.Keypad2, KeyCode.Keypad3,				// TODO: maybe move it in game manager instead + make it private
		KeyCode.Keypad4, KeyCode.Keypad5, KeyCode.Keypad6, KeyCode.Keypad7, KeyCode.Keypad8, KeyCode.Keypad9};										//TODO: change for arduino board input

	public List<int> acceptedInputKeysArd = new List<int> { 49, 50, 51, 52, 53, 54, 55, 56, 57 };		// TODO: hide

	//SerialPort sp = new SerialPort("COM6", 9600);
	SerialPort sp = new SerialPort();
	GameManager gameManager;
	// Use this for initialization

	public List<int> arduinoInput = new List<int>();
	public int read;
	public List<int> arduinoDiscard = new List<int> ();

	void Awake() {
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		sp.PortName = "COM6";
		sp.BaudRate = 9600;
		sp.ReadTimeout = 1;
		sp.Open ();
	}

	// Update is called once per frame
	void Update () {
		if (gameManager.paused) {
			arduinoDiscard.Clear ();
			read = ArduinoInputRead ();
		} else {
			//sp.DiscardOutBuffer ();
			try {
				arduinoDiscard.Add (sp.ReadByte()	);
				read = -1;
			} catch (System.Exception) {
			}
		}
	}

	public int ArduinoInputRead() {
		if (sp.IsOpen) {
			ClearArduinoInput();
			try {
				arduinoInput.Add (sp.ReadByte ());
				arduinoInput.Remove (10);
				arduinoInput.Remove (13);
				Debug.Log("test1");
				return arduinoInput [arduinoInput.Count - 1];
			} catch (System.Exception) {
				return -1;
			}
		} else
			return -2;
	}
		
	public void ClearArduinoInput() {
		sp.DiscardOutBuffer();
		arduinoInput.Clear ();
	}

	public bool GetArdKeyDown(int key) {
		if (read == key) {
			return true;
		}
		else {
			return false;
		}
	}

	/*public bool CompareWithArduinoInput (int read) {
		ClearArduinoInput ();
		ArduinoInputRead ();
		if (arduinoInput.Count > 0) {
			if (arduinoInput [0] == read) {		// if the first (and only) element of the list is equal to the read, return true
				return true;
			} else {
				return false;
			}
		} else {
			return false;
		}
	}

	public bool ArduinoInputExists() {
		if (arduinoInput.Count > 0) {
			return true;
		} else {
			return false;
		}
	}*/
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
