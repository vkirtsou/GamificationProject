using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public GameObject wrongAnswerPopup, correctAnswerPopup, darkerTint;
	public Text scoreUIText;

	public bool isPopupActive = false;		// TODO: make it private
	public bool muscleSkipped = false;		// TODO: make it private

	public int tries = 0;
	public int fullPoints = 300;

	private GameManager gameManager;
	public AudioSource audiosource;

	public AudioClip audioWrongAnswer;
	public AudioClip audioCorrectAnswer;
	//public AudioClip audioClickButton;
	//public AudioClip audioHoverButton;

	// Use this for initialization

	void Start () {
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		scoreUIText = GameObject.Find("ScorePanel").GetComponentInChildren<Text> ();
		ShowScorePoints ();
		audiosource = GetComponent<AudioSource> ();
	}

	public void ShowWrongAnswerPopup() {
		audiosource.clip = audioWrongAnswer;
		audiosource.Play ();
		wrongAnswerPopup.SetActive (true);
		darkerTint.SetActive (true);
		isPopupActive = true;
		gameManager.PauseGame ();
		tries++;
	}

	public void RetryForMuscle() {
		wrongAnswerPopup.SetActive (false);
		darkerTint.SetActive (false);
		isPopupActive = false;

	}

	public void SkipMuscle() {
		wrongAnswerPopup.SetActive (false);
		darkerTint.SetActive (false);
		isPopupActive = false;
		muscleSkipped = true;
	}

	public void ShowCorrectAnswerPopup() {
		// play sound
		audiosource.clip = audioCorrectAnswer;
		audiosource.Play ();
		correctAnswerPopup.SetActive (true);
		wrongAnswerPopup.SetActive (false);
		darkerTint.SetActive (false);
		isPopupActive = true;
		//tries++;
		Debug.Log ("Correct answer with " + tries + " tries!");
		// add score depending on tries
		StartCoroutine ("WaitAndDisappear");
		switch (tries) {									// add a score depending on the tries that it took to pass 
		case 0:
			gameManager.AddScorePoints (fullPoints);
			ShowScorePoints ();
			break;
		case 1:
			gameManager.AddScorePoints (fullPoints - 50);
			ShowScorePoints ();
			break;
		case 2:
			gameManager.AddScorePoints (fullPoints - 100);
			ShowScorePoints ();
			break;
		default:
			gameManager.AddScorePoints (fullPoints - 150);
			ShowScorePoints ();
			break;
		}

	}

	IEnumerator WaitAndDisappear() {
		yield return new WaitForSeconds (1f);
		isPopupActive = false;
		correctAnswerPopup.SetActive (false);
		darkerTint.SetActive (false);

	}

	public void ShowScorePoints() {
		scoreUIText.text = "Score: " + gameManager.scorePoints;
	}
}
