using UnityEngine;
using System.Collections;

public class MuscleCollision : MonoBehaviour {

	// Use this for initialization

	public enum FacialMuscleType {
		Muscle1,
		Muscle2,
		Muscle3,
		Muscle4,
		Muscle5,
		Muscle6
	}

	public GameObject muscle;
	public FacialMuscleType facialMuscle;
	private IEnumerator coroutine;

	void Start () {
		muscle = GameObject.Find (facialMuscle.ToString());
	}

	void OnTriggerEnter() {
		StartCoroutine("WaitAndChangeColor2");
	}
		
	private IEnumerator WaitAndChangeColor2() {
		Debug.Log (muscle.ToString ());
		Color defaultColor;
		defaultColor = muscle.GetComponent<Renderer> ().material.color;	// the current color of the muscle (ie. green)
		muscle.GetComponent<Renderer> ().material.color = Color.red;		// turn muscle red
		yield return new WaitForSeconds (2f);							// wait 2 seconds
		muscle.GetComponent<Renderer> ().material.color = defaultColor;	// turn muscle back to original color
	}
}
