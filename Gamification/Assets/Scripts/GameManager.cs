using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour {

	public bool paused = false;
	//public FirstPersonController fpc;
	public int scorePoints = 0;


	void Awake() {
		//fpc = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
	}

	public void PauseGame() {
		if (!paused) {
			paused = true;
			Time.timeScale = 0;
			Cursor.visible = true;
			DisableMouse ();
		}
	}

	public void ResumeGame() {
		if (paused) {
			paused = false;
			Time.timeScale = 1;
			Cursor.visible = false;
			EnableMouse ();
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

	public void AddScorePoints(int points) {
		scorePoints += points;
	}
}
