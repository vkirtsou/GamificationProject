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
		
	public KeyCode correspondingNumKey;				// the corresponding numeric key for this muscle
	public int correspondingArdKey; 				// the corresponding arduino key for this muscle

	public GameObject muscle;
	public FacialMuscleType facialMuscle;
	public int muscleNumber; 						
	public bool muscleCollided = false; 			// true if the muscle collided with some object
	public bool muscleActivated = false;			// true if the muscle was activated by the user
	public bool paused = false;

	private GameManager gameManager;					
	private Rigidbody rigidb;						
	private UIManager uiManager;						
	public InputManager inputManager;

	private List<KeyCode> acceptedInputKeys; 		// KeyCode Type when using the numeric keypad for input
	private List<KeyCode> invalidInputKeys;		


	private List<int> acceptedInputKeysArd;			// int type when using arduino for input
	public List<int> invalidInputKeysArd;

	public AudioClip audioBubblePop;
	//public AudioClip[] audiosForMuscles = new AudioClip;
	public AudioSource audioSource;

	void Awake() {
		muscle = GameObject.Find (facialMuscle.ToString());	// the muscle we selected from unity inspector
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		rigidb = GameObject.Find ("FPSController").GetComponent<Rigidbody> ();
		uiManager = GameObject.Find ("Canvas").GetComponent<UIManager> ();
		inputManager = GameObject.Find ("GameManager").GetComponent<InputManager> ();
		audioSource = GetComponent<AudioSource> ();
		acceptedInputKeys = GameObject.Find ("GameManager").GetComponent<InputManager> ().acceptedInputKeys;
		acceptedInputKeysArd = GameObject.Find ("GameManager").GetComponent<InputManager> ().acceptedInputKeysArd;

	}

	void Start () {
		invalidInputKeys = new List<KeyCode>(acceptedInputKeys);
		invalidInputKeysArd = new List<int> (acceptedInputKeysArd);

		if (muscle.gameObject.name == "Muscle1") {		// add assorted numeric key for the correct muscle 
			//correspondingNumKey = acceptedInputKeys[0];
			muscleNumber = 0;						
		}  else if (muscle.gameObject.name == "Muscle2") {
			muscleNumber = 1;
		}  else if (muscle.gameObject.name == "Muscle3") {
			muscleNumber = 2;
		}  else if (muscle.gameObject.name == "Muscle4") {
			muscleNumber = 3;
		}  else if (muscle.gameObject.name == "Muscle5") {
			muscleNumber = 4;
		}  else if (muscle.gameObject.name == "Muscle6") {
			muscleNumber = 5;
		}  else if (muscle.gameObject.name == "Muscle7") {
			muscleNumber = 6;
		}  else if (muscle.gameObject.name == "Muscle8") {
			muscleNumber = 7;
		}  else if (muscle.gameObject.name == "Muscle9") {
			muscleNumber = 8;
		}
		correspondingNumKey = acceptedInputKeys[muscleNumber];			// the corresponding key to activate the muscle
		invalidInputKeys.Remove (correspondingNumKey);					// remove the correct key from the list of the invalid keys

		correspondingArdKey = acceptedInputKeysArd[muscleNumber];			// the corresponding key to activate the muscle
		invalidInputKeysArd.Remove (correspondingArdKey);					// remove the correct key from the list of the invalid keys

		//audioSource.clip = audiosForMuscles [muscleNumber];				// the audio to play for each muscle
		audioSource.clip = audioBubblePop;
	}

	void Update() {
		if (muscleCollided && !muscleActivated) {			// if the muscle collided and hasn't been activated (responded from user) yet i.e. waiting input from user
			if (Input.GetKeyDown (correspondingNumKey) || inputManager.GetArdKeyDown(correspondingArdKey)) {	// if the key pressed is the correct one --> moved the correct muscle
				Debug.Log (muscleNumber);					// TODO: Delete.
				muscleActivated = true;						// mark the muscle as activated to avoid multiple checkings of collision
			} else {										// (Wrong answer)
					foreach (KeyCode wrongKey in invalidInputKeys) {	
					if (Input.GetKeyDown (wrongKey)) {
						Debug.Log ("wrong answer");
						//if (gameManager.muscleNumbersCurrentlyRed.Count <= 1 || !gameManager.muscleNumbersCurrentlyRed.Contains(wrongKey)) {			
						if (gameManager.muscleNumbersCurrentlyRed.Count <= 1) {			// if more than one muscle is red/collided with, dont show wrong popup
							uiManager.ShowWrongAnswerPopup ();							// Popup with options: Retry + Skip
							//gameManager.PauseGame();

						}							
					}
				}		// FOR ARDUINO:
				foreach (int wrongKey in invalidInputKeysArd) {	
					if (inputManager.GetArdKeyDown(wrongKey) && inputManager.read != -1) {
						Debug.Log ("wrong answer");
						//if (gameManager.muscleNumbersCurrentlyRed.Count <= 1 || !gameManager.muscleNumbersCurrentlyRed.Contains(wrongKey)) {			
						if (gameManager.muscleNumbersCurrentlyRed.Count <= 1 || !gameManager.muscleNumbersCurrentlyRedArd.Contains(wrongKey)) {			// if more than one muscle is red/collided with, dont show wrong popup
							uiManager.ShowWrongAnswerPopup ();							// Popup with options: Retry + Skip
							//gameManager.PauseGame();

						}							
					}
				}
			}
		}
	}


	void OnTriggerEnter(Collider co) {
		if (co.CompareTag("Diamond")) {						// if it collides with a diamond TODO: add more tags if needed
			audioSource.Play();								// play the collision audio for the muscle
			gameManager.AddRedMuscleToList(muscleNumber);	// add the muscle that is red in the list of "red" (collided) muscles
			muscleCollided = true;
			gameManager.PauseGame();				// Freeze movement + Camera
			StartCoroutine("WaitToActivateMuscle");
		}
	}

	public void FakeCollidingForTesting() {
		audioSource.Play();								// play the collision audio for the muscle
		gameManager.AddRedMuscleToList(muscleNumber);	// add the muscle that is red in the list of "red" (collided) muscles
		muscleCollided = true;
		gameManager.PauseGame();				// Freeze movement + Camera
		StartCoroutine("WaitToActivateMuscle");
	}

	private IEnumerator WaitToActivateMuscle() {
		Debug.Log (muscle.ToString ());

		//Color defaultColor;
		//defaultColor = muscle.GetComponent<Renderer> ().material.color;		// the current color of the muscle (ie. green)
		muscle.GetComponent<Renderer> ().material.color = Color.red;		// turn muscle red
		yield return new WaitUntil(() => muscleActivated || uiManager.muscleSkipped);					// hang for player input --> activate the correct muscle

		gameManager.RemoveRedMuscleFromList(muscleNumber); 			// remove the muscle number from the list of the "red" (collided) muscles
		if (gameManager.muscleNumbersCurrentlyRed.Count == 0) {			// if no more muscles are red, resume
			gameManager.ResumeGame();									// Resume movement + camera
		}
		if (muscleActivated) {										// give the correct answer popup + points only if the user activated the muscle.
			uiManager.ShowCorrectAnswerPopup ();		
			muscleActivated = false;					// reset
		}
		muscle.GetComponent<Renderer> ().material.color = Color.white;		// turn muscle back to original color
		//muscle.GetComponent<Renderer> ().material.color = defaultColor;		// turn muscle back to original color
		muscleCollided = false;							// reset
		uiManager.muscleSkipped = false;				// reset
		uiManager.tries = 0;							// reset

	}




}
