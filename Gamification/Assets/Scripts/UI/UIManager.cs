using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	public GameObject wrongAnswerPopup;
	public GameObject correctAnswerPopup;
	public GameObject darkerTint;
	public bool isPopupActive = false;		// TODO: make it private
	public bool muscleSkipped = false;		// TODO: make it private

	public int tries = 0;
	// Use this for initialization

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowWrongAnswerPopup() {
		wrongAnswerPopup.SetActive (true);
		darkerTint.SetActive (true);
		isPopupActive = true;
	}

	public void RetryForMuscle() {
		wrongAnswerPopup.SetActive (false);
		darkerTint.SetActive (false);
		isPopupActive = false;
		tries++;
	}

	public void SkipMuscle() {
		wrongAnswerPopup.SetActive (false);
		darkerTint.SetActive (false);
		isPopupActive = false;
		muscleSkipped = true;
	}

	public void ShowCorrectAnswerPopup() {
		correctAnswerPopup.SetActive (true);
		darkerTint.SetActive (true);
		isPopupActive = true;
		tries++;
		Debug.Log ("Correct answer with " + tries + " tries!");
		StartCoroutine ("WaitAndDisappear");
	}

	IEnumerator WaitAndDisappear() {
		yield return new WaitForSeconds (2f);
		correctAnswerPopup.SetActive (false);
		darkerTint.SetActive (false);
		isPopupActive = false;
	}
}
