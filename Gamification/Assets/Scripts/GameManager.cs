using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour {

	public bool paused = false;
	public int scorePoints = 0;

	// START //
	// for debugging. TODO: Delete.
	/*public MuscleCollision muscle1;
	public MuscleCollision muscle2;
	public MuscleCollision muscle3;
	public MuscleCollision muscle4;
	public MuscleCollision muscle5;
	public MuscleCollision muscle6;
	public MuscleCollision muscle7;
	public MuscleCollision muscle8;
	public MuscleCollision muscle9;
	public GameManager gameManager;
	// END //
*/
	public List<int> muscleNumbersCurrentlyRed = new List<int> ();
	public List<int> muscleNumbersCurrentlyRedArd = new List<int>();

	void Awake() {
		// START //
		// for debugging. TODO: Delete.
		/*muscle1 = GameObject.Find("Muscle1").GetComponent<MuscleCollision>();
		muscle2 = GameObject.Find("Muscle2").GetComponent<MuscleCollision>();
		muscle3 = GameObject.Find("Muscle3").GetComponent<MuscleCollision>();
		muscle4 = GameObject.Find("Muscle4").GetComponent<MuscleCollision>();
		muscle5 = GameObject.Find("Muscle5").GetComponent<MuscleCollision>();
		muscle6 = GameObject.Find("Muscle6").GetComponent<MuscleCollision>();
		muscle7 = GameObject.Find("Muscle7").GetComponent<MuscleCollision>();
		muscle8 = GameObject.Find("Muscle8").GetComponent<MuscleCollision>();
		muscle9 = GameObject.Find("Muscle9").GetComponent<MuscleCollision>();*/
		// END //

	}

	void Update() {
		/*
		// START //
		// TODO: DELETE. ONLY FOR DEBUGGING /*
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			AddRedMuscleToList(0);	// add the muscle that is red in the list of "red" (collided) muscles
			//muscle1.audioSource.Play ();
			//muscle1.StartCoroutine ("WaitToActivateMuscle");
			//muscle1.muscleCollided = true;
			muscle1.FakeCollidingForTesting ();
		}
		if (Input.GetKeyDown(KeyCode.Alpha2)) {
			AddRedMuscleToList(1);
			muscle2.FakeCollidingForTesting ();
		}
		if (Input.GetKeyDown(KeyCode.Alpha3)) {
			AddRedMuscleToList(2);
			muscle3.FakeCollidingForTesting ();
		}

		if (Input.GetKeyDown(KeyCode.Alpha4)) {
			AddRedMuscleToList(3);
			muscle4.FakeCollidingForTesting ();
		}
		if (Input.GetKeyDown(KeyCode.Alpha5)) {
			AddRedMuscleToList(4);
			muscle5.FakeCollidingForTesting ();
		}
		if (Input.GetKeyDown(KeyCode.Alpha6)) {
			AddRedMuscleToList(5);
			muscle6.FakeCollidingForTesting ();
		}
		if (Input.GetKeyDown(KeyCode.Alpha7)) {
			AddRedMuscleToList(6);
			muscle7.FakeCollidingForTesting ();
		}
		if (Input.GetKeyDown(KeyCode.Alpha8)) {
			AddRedMuscleToList(7);
			muscle8.FakeCollidingForTesting ();
		}
		if (Input.GetKeyDown(KeyCode.Alpha9)) {
			AddRedMuscleToList(8);
			muscle9.FakeCollidingForTesting ();
		}
		// END //
		*/
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

	public void AddRedMuscleToList(int i) {
		if (!muscleNumbersCurrentlyRed.Contains(i)) {
			muscleNumbersCurrentlyRed.Add (i);
			int arduinoEquivalent = 49 + i;		// ascii 
			muscleNumbersCurrentlyRedArd.Add (arduinoEquivalent);
		}
	}

	public void RemoveRedMuscleFromList(int i) {
		if (muscleNumbersCurrentlyRed.Contains (i)) {
			muscleNumbersCurrentlyRed.Remove (i);
			int arduinoEquivalent = 49 + i;
			muscleNumbersCurrentlyRedArd.Remove (arduinoEquivalent);
		}
	}
}
