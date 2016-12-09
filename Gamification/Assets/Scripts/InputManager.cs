﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;

public class InputManager : MonoBehaviour {

	[HideInInspector] public List<KeyCode> acceptedInputKeys = new List<KeyCode> {KeyCode.Keypad1, KeyCode.Keypad2, KeyCode.Keypad3,				// TODO: maybe move it in game manager instead + make it private
		KeyCode.Keypad4, KeyCode.Keypad5, KeyCode.Keypad6, KeyCode.Keypad7, KeyCode.Keypad8, KeyCode.Keypad9};										

	[HideInInspector] public List<int> acceptedInputKeysArd = new List<int> { 49, 50, 51, 52, 53, 54, 55, 56, 57 };	

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
				//Debug.Log("test1");
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
		
}
	
