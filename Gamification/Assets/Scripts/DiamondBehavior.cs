using UnityEngine;
using System.Collections;

public class DiamondBehavior : MonoBehaviour {

	public int rotationType;
	public int movingType;
	public float speed;

	Vector3 style1 = new Vector3(1f, 1f, 0f);
	Vector3 style2 = new Vector3 (0f, 1f, 1f);
	Vector3 style3 = new Vector3(1f, -1f, 0f);
	Vector3 style4 = new Vector3 (0f, -1f, 1f);

	AudioSource audioSource;
	// Use this for initialization
	AudioClip bubblePop;
	void Start () {
		DecideRotationRandomness();
		audioSource = GetComponent<AudioSource> ();
		bubblePop = (AudioClip)Resources.Load ("T_bubblePop");
	}
	
	// Update is called once per frame
	void Update () {
		RotateDiamondRandom ();
	}

	void DecideRotationRandomness() {
		movingType = (int)Random.Range (4, 8);
		rotationType = (int)Random.Range(4, 8);
		speed = Random.Range (15, 35);
	}

	void MoveObjectsRandom() {
		
	}

	void RotateDiamondRandom() {			// random rotation for each cube 

		switch (rotationType) {				// currently working with cases 4 to 7 only
		case 0:
			gameObject.transform.Rotate (Vector3.right, Time.deltaTime * speed);
			break;
		case 1:
			gameObject.transform.Rotate (Vector3.up, Time.deltaTime * speed);
			break;
		case 2:
			gameObject.transform.Rotate (Vector3.down, Time.deltaTime * speed);
			break;
		case 3:
			gameObject.transform.Rotate (Vector3.left, Time.deltaTime * speed);
			break;
		case 4:
			gameObject.transform.Rotate (style1, Time.deltaTime * speed);
			break;
		case 5:
			gameObject.transform.Rotate (style2, Time.deltaTime * speed);
			break;
		case 6:
			gameObject.transform.Rotate (style3, Time.deltaTime * speed);
			break;
		case 7:
			gameObject.transform.Rotate (style4, Time.deltaTime * speed);
			break;
		default:
			Debug.Log ("oops");
			break;
		}
	}

	void OnTriggerEnter(Collider co) {
		if (co.tag.Equals ("Player")) {
			this.GetComponent<Renderer> ().material.color = Color.red;
			Debug.Log ("player collided!");
			StartCoroutine ("WaitAndDestroy");
		}

		if (co.tag.Equals ("Muscle")) {
			Debug.Log ("muscle collided!");
			this.GetComponent<Renderer> ().material.color = Color.blue;

			StartCoroutine ("WaitAndDestroy");
		}
	}

	IEnumerator WaitAndDestroy() {
		yield return new WaitForSeconds (0.5f);
		audioSource.Play ();
		Destroy (gameObject);
	}
}
