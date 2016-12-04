using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;

public class MuscleCollision : MonoBehaviour {

	// Use this for initialization

	public enum FacialMuscleType {
		Muscle1, Muscle2, Muscle3,
		Muscle4, Muscle5, Muscle6,
		Muscle7, Muscle8, Muscle9
	}

	public KeyCode correspondingNumKey;
	public GameObject muscle;
	public FacialMuscleType facialMuscle;
	public int muscleNumber; 						// TODO: For debug purposes. delete
	public bool muscleCollided = false; 			// true if the muscle collided with some object
	public bool muscleActivated = false;			// true if the muscle was activated by the user
	public bool paused = false;
	public int i = 0;								// TODO: delete?? needed locally in Update() 

	public GameManager gameManager;					// TODO: make it private
	public Rigidbody rigidb;						// TODO: make it private
	public UIManager uiManager;						// TODO: make it private

	public List<KeyCode> acceptedInputKeys = new List<KeyCode> {KeyCode.Keypad1, KeyCode.Keypad2, KeyCode.Keypad3,				// TODO: maybe move it in game manager instead + make it private
		KeyCode.Keypad4, KeyCode.Keypad5, KeyCode.Keypad6, KeyCode.Keypad7, KeyCode.Keypad8, KeyCode.Keypad9};

	public List<KeyCode> invalidInputKeys;			// TODO: make it private

	//public KeyCode[] acceptedInputKeys = new KeyCode[9] { KeyCode.Keypad1, KeyCode.Keypad2, KeyCode.Keypad3,
	//	KeyCode.Keypad4, KeyCode.Keypad5, KeyCode.Keypad6, KeyCode.Keypad7, KeyCode.Keypad8, KeyCode.Keypad9 };	

	//public KeyCode[] invalidInputKeys = new KeyCode[9];
	void Awake() {
		muscle = GameObject.Find (facialMuscle.ToString());	// the muscle we selected from unity inspector
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		rigidb = GameObject.Find ("FPSController").GetComponent<Rigidbody> ();
		uiManager = GameObject.Find ("Canvas").GetComponent<UIManager> ();

	}

	void Start () {
		invalidInputKeys = new List<KeyCode>(acceptedInputKeys);			

		if (muscle.gameObject.name == "Muscle1") {		// add assorted numeric key for the correct muscle TODO: change for arduino board input
			correspondingNumKey = acceptedInputKeys[0];
			muscleNumber = 1;							// TODO: Delete.
			// TODO: add audiosource
		}  else if (muscle.gameObject.name == "Muscle2") {
			correspondingNumKey = acceptedInputKeys[1];
			muscleNumber = 2;
		}  else if (muscle.gameObject.name == "Muscle3") {
			correspondingNumKey = acceptedInputKeys[2];
			muscleNumber = 3;
		}  else if (muscle.gameObject.name == "Muscle4") {
			correspondingNumKey = acceptedInputKeys[3];
			muscleNumber = 4;
		}  else if (muscle.gameObject.name == "Muscle5") {
			correspondingNumKey = acceptedInputKeys[4];
			muscleNumber = 5;
		}  else if (muscle.gameObject.name == "Muscle6") {
			correspondingNumKey = acceptedInputKeys[5];
			muscleNumber = 6;
		}  else if (muscle.gameObject.name == "Muscle7") {
			correspondingNumKey = acceptedInputKeys[6];
			muscleNumber = 7;
		}  else if (muscle.gameObject.name == "Muscle8") {
			correspondingNumKey = acceptedInputKeys[7];
			muscleNumber = 8;
		}  else if (muscle.gameObject.name == "Muscle9") {
			correspondingNumKey = acceptedInputKeys[8];
			muscleNumber = 9;
		}
		invalidInputKeys.Remove (correspondingNumKey);
	}

	void Update() {
		if (muscleCollided && !muscleActivated) {			// if the muscle collided and hasn't been activated ("green") yet i.e. waiting input from user
			//gameManager.tries = 0;
			if (Input.GetKeyDown (correspondingNumKey)) {	// if the key pressed is the correct one --> moved the correct muscle
				Debug.Log (muscleNumber);
				muscleActivated = true;						// mark the muscle as activated to avoid multiple checkings of collision
			} else {
				foreach (KeyCode wrongKey in invalidInputKeys) {	// (Wrong answer)
					if (Input.GetKeyDown (wrongKey)) {
						uiManager.ShowWrongAnswerPopup ();	// Popup with options: Retry + Skip
						// Play sound
						//gameManager.tries++;
					}
				}
			}
		}
	}

	void OnTriggerEnter(Collider co) {
		if (co.CompareTag("Diamond")) {						// if it collides with a diamond TODO: add more tags if needed
			StartCoroutine("WaitToActivateMuscle");
		}
	}
		
	private IEnumerator WaitToActivateMuscle() {
		Debug.Log (muscle.ToString ());
		muscleCollided = true;
		gameManager.PauseGame();				// Freeze movement + Camera
		Color defaultColor;
		defaultColor = muscle.GetComponent<Renderer> ().material.color;		// the current color of the muscle (ie. green)
		muscle.GetComponent<Renderer> ().material.color = Color.red;		// turn muscle red
		yield return new WaitUntil(() => muscleActivated || uiManager.muscleSkipped);					// hang for player input --> activate the correct muscle

		gameManager.ResumeGame();				// Resume movement + camera
		uiManager.ShowCorrectAnswerPopup();
		muscle.GetComponent<Renderer> ().material.color = defaultColor;		// turn muscle back to original color
		muscleCollided = false;			
		muscleActivated = false;
		uiManager.muscleSkipped = false;
		uiManager.tries = 0;

		// TODO: SHOW A POPUP (when? shouldnt be spammed)
	}




}
