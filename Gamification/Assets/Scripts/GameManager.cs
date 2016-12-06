using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour {

	public bool paused = false;
	//public FirstPersonController fpc;
	public int scorePoints = 0;
	public MuscleCollision muscle1;
	public MuscleCollision muscle2;
	public MuscleCollision muscle3;
	public MuscleCollision muscle4;
	public MuscleCollision muscle5;
	public MuscleCollision muscle6;
	public MuscleCollision muscle7;
	public MuscleCollision muscle8;
	public MuscleCollision muscle9;

	public List<int> musclesCurrentlyRed = new List<int> ();

	void Awake() {
		//fpc = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
		muscle1 = GameObject.Find("Muscle1").GetComponent<MuscleCollision>();
		muscle2 = GameObject.Find("Muscle2").GetComponent<MuscleCollision>();
		muscle3 = GameObject.Find("Muscle3").GetComponent<MuscleCollision>();
		muscle4 = GameObject.Find("Muscle4").GetComponent<MuscleCollision>();
		muscle5 = GameObject.Find("Muscle5").GetComponent<MuscleCollision>();
		muscle6 = GameObject.Find("Muscle6").GetComponent<MuscleCollision>();
		muscle7 = GameObject.Find("Muscle7").GetComponent<MuscleCollision>();
		muscle8 = GameObject.Find("Muscle8").GetComponent<MuscleCollision>();
		muscle9 = GameObject.Find("Muscle9").GetComponent<MuscleCollision>();
	}

	void Update() {
		// TODO: DELETE. ONLY FOR DEBUGGING
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			muscle1.audioSource.Play ();
			muscle1.StartCoroutine ("WaitToActivateMuscle");
			muscle1.muscleCollided = true;
		}
		if (Input.GetKeyDown(KeyCode.Alpha2)) {
			muscle2.audioSource.Play ();
			muscle2.StartCoroutine ("WaitToActivateMuscle");
			muscle2.muscleCollided = true;
		}
		if (Input.GetKeyDown(KeyCode.Alpha3)) {
			muscle3.audioSource.Play ();
			muscle3.StartCoroutine ("WaitToActivateMuscle");
			muscle3.muscleCollided = true;
		}

		if (Input.GetKeyDown(KeyCode.Alpha4)) {
			muscle4.audioSource.Play ();
			muscle4.StartCoroutine ("WaitToActivateMuscle");
			muscle4.muscleCollided = true;
		}
		if (Input.GetKeyDown(KeyCode.Alpha5)) {
			muscle5.audioSource.Play ();
			muscle5.StartCoroutine ("WaitToActivateMuscle");
			muscle5.muscleCollided = true;
		}
		if (Input.GetKeyDown(KeyCode.Alpha6)) {
			muscle6.audioSource.Play ();
			muscle6.StartCoroutine ("WaitToActivateMuscle");
			muscle6.muscleCollided = true;
		}
		if (Input.GetKeyDown(KeyCode.Alpha7)) {
			muscle7.audioSource.Play ();
			muscle7.StartCoroutine ("WaitToActivateMuscle");
			muscle7.muscleCollided = true;
		}
		if (Input.GetKeyDown(KeyCode.Alpha8)) {
			muscle8.audioSource.Play ();
			muscle8.StartCoroutine ("WaitToActivateMuscle");
			muscle8.muscleCollided = true;
		}
		if (Input.GetKeyDown(KeyCode.Alpha9)) {
			muscle9.audioSource.Play ();
			muscle9.StartCoroutine ("WaitToActivateMuscle");
			muscle9.muscleCollided = true;
		}

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
