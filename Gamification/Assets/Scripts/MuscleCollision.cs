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

	GameObject mus1, mus2, mus3, mus4, mus5, mus6;
	public FacialMuscleType facialMuscle;
	private IEnumerator coroutine;

	void Start () {
		mus1 = GameObject.Find ("Muscle1");
		mus2 = GameObject.Find ("Muscle2");
		mus3 = GameObject.Find ("Muscle3");
		mus4 = GameObject.Find ("Muscle4");
		mus5 = GameObject.Find ("Muscle5");
		mus6 = GameObject.Find ("Muscle6");

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter() {
		
		switch (facialMuscle) {
		case FacialMuscleType.Muscle1:
			coroutine = WaitAndChangeColor (mus1);
			StartCoroutine (coroutine);
			Debug.Log ("Muscle 1");
			break;
		case FacialMuscleType.Muscle2:
			coroutine = WaitAndChangeColor (mus2);
			StartCoroutine (coroutine);
			Debug.Log ("Muscle 2");
			break;
		case FacialMuscleType.Muscle3:
			coroutine = WaitAndChangeColor (mus3);
			StartCoroutine (coroutine);
			Debug.Log ("Muscle 3");
			break;
		case FacialMuscleType.Muscle4:
			coroutine = WaitAndChangeColor (mus4);
			StartCoroutine (coroutine);
			Debug.Log ("Muscle 4");
			break;
		case FacialMuscleType.Muscle5:
			coroutine = WaitAndChangeColor (mus5);
			StartCoroutine (coroutine);
			Debug.Log ("Muscle 5");
			break;
		case FacialMuscleType.Muscle6:
			coroutine = WaitAndChangeColor (mus6);
			StartCoroutine (coroutine);
			Debug.Log ("Muscle 6");
			break;
		default:
			break;
		}
	}

	private IEnumerator WaitAndChangeColor(GameObject mus) {
		Color defaultColor;
		defaultColor = mus.GetComponent<Renderer> ().material.color;	// the current color of the muscle (ie. green)
		mus.GetComponent<Renderer> ().material.color = Color.red;		// turn muscle red
		yield return new WaitForSeconds (2f);							// wait 2 seconds
		mus.GetComponent<Renderer> ().material.color = defaultColor;	// turn muscle back to original color
	}
}
