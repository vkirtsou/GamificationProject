using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour {

	public bool paused = false;
	public FirstPersonController fpc;

	void Awake() {
		fpc = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PauseGame() {
		if (!paused) {
			paused = true;
			Time.timeScale = 0;
			Cursor.visible = true;
		}
	}

	public void ResumeGame() {
		if (paused) {
			paused = false;
			Time.timeScale = 1;
			Cursor.visible = false;
		}
	}

	public void DisableMouse() {
		if (MouseLook.mouseEnabled) {
			MouseLook.mouseEnabled = false;
		}
	}

	public void EnableMouse() {
		if (!MouseLook.mouseEnabled) {
			MouseLook.mouseEnabled = true;
		}
	}
}
