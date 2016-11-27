using UnityEngine;
using System.Collections;
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

	public GameManager gameManager;
	public Rigidbody rigidb;

	void Awake() {
		muscle = GameObject.Find (facialMuscle.ToString());	// the muscle we selected from unity inspector
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		rigidb = GameObject.Find ("FPSController").GetComponent<Rigidbody> ();
	}

	void Start () {
		if (muscle.gameObject.name == "Muscle1") {		// add assorted numeric key for the correct muscle TODO: change for arduino board input
			correspondingNumKey = KeyCode.Keypad1;
			muscleNumber = 1;
		}  else if (muscle.gameObject.name == "Muscle2") {
			correspondingNumKey = KeyCode.Keypad2;
			muscleNumber = 2;
		}  else if (muscle.gameObject.name == "Muscle3") {
			correspondingNumKey = KeyCode.Keypad3;
			muscleNumber = 3;
		}  else if (muscle.gameObject.name == "Muscle4") {
			correspondingNumKey = KeyCode.Keypad4;
			muscleNumber = 4;
		}  else if (muscle.gameObject.name == "Muscle5") {
			correspondingNumKey = KeyCode.Keypad5;
			muscleNumber = 5;
		}  else if (muscle.gameObject.name == "Muscle6") {
			correspondingNumKey = KeyCode.Keypad6;
			muscleNumber = 6;
		}  else if (muscle.gameObject.name == "Muscle7") {
			correspondingNumKey = KeyCode.Keypad7;
			muscleNumber = 7;
		}  else if (muscle.gameObject.name == "Muscle8") {
			correspondingNumKey = KeyCode.Keypad8;
			muscleNumber = 8;
		}  else if (muscle.gameObject.name == "Muscle9") {
			correspondingNumKey = KeyCode.Keypad9;
			muscleNumber = 9;
		}
	}

	void Update() {
		if (muscleCollided && !muscleActivated) {			// if the muscle collided and hasn't been activated ("green") yet i.e. waiting input from user
			if (Input.GetKeyDown (correspondingNumKey)) {	// if the key pressed is the correct one --> moved the correct muscle
				Debug.Log (muscleNumber);
				muscleActivated = true;						// mark the muscle as activated to avoid multiple checkings of collision
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
		gameManager.PauseGame();				// Freeze movement
		gameManager.DisableMouse ();			// Freeze camera
		Color defaultColor;
		defaultColor = muscle.GetComponent<Renderer> ().material.color;		// the current color of the muscle (ie. green)
		muscle.GetComponent<Renderer> ().material.color = Color.red;		// turn muscle red
		yield return new WaitUntil(() => muscleActivated);					// hang for player input --> activate the correct muscle

		gameManager.ResumeGame();				// Resume movement
		gameManager.EnableMouse ();				// resume camera
		muscle.GetComponent<Renderer> ().material.color = defaultColor;		// turn muscle back to original color
		muscleCollided = false;			
		muscleActivated = false;
		// TODO: SHOW A POPUP (when? shouldnt be spammed)
	}




}
