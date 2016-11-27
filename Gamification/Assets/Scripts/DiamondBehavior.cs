using UnityEngine;
using System.Collections;

public class DiamondBehavior : MonoBehaviour {

	public int rotationType;
	public float speed;

	// Use this for initialization
	void Start () {
		DecideRotationRandomness();
	}
	
	// Update is called once per frame
	void Update () {
		RotateDiamondRandom ();
	}

	void DecideRotationRandomness() {
		rotationType = (int)Random.Range(4, 8);
		speed = Random.Range (15, 35);
	}

	void RotateDiamondRandom() {			// random rotation for each cube 
		Vector3 rotationStyle1 = new Vector3(1f, 1f, 0f);
		Vector3 rotationStyle2 = new Vector3 (0f, 1f, 1f);
		Vector3 rotationStyle3 = new Vector3(1f, -1f, 0f);
		Vector3 rotationStyle4 = new Vector3 (0f, -1f, 1f);

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
			gameObject.transform.Rotate (rotationStyle1, Time.deltaTime * speed);
			break;
		case 5:
			gameObject.transform.Rotate (rotationStyle2, Time.deltaTime * speed);
			break;
		case 6:
			gameObject.transform.Rotate (rotationStyle3, Time.deltaTime * speed);
			break;
		case 7:
			gameObject.transform.Rotate (rotationStyle4, Time.deltaTime * speed);
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
			Destroy (gameObject);
		}

		if (co.tag.Equals ("Muscle")) {
			Debug.Log ("muscle collided!");
			this.GetComponent<Renderer> ().material.color = Color.blue;
			Destroy (gameObject);
		}
	}
}
