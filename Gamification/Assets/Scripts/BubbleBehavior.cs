using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BubbleBehavior : MonoBehaviour {

	public int movingType = -1;
	public float speed;

	// Bubble properties
	private Rigidbody bubbleRigid;
	private Vector3 bubblePosition, positionCorrection;
	//public Transform parentTransform;

	// Variables (or constants?) for random motion
	//public float movementSpeed = 30f;			// movement speed of the bubble
	public float maxVelocity = 0.1f;			// maximum accepted velocity of the bubble
	public float space = 0.005f; 					// What's that for? i think for space between the bubble + wall

	BubbleCreation bubbleCreation;

	public Vector3 min, max, direction;

	void Start () {
		direction = GetMovementStyle ();							// the initial direction vector that the bubble will move towards
		speed = Random.Range (15, 35);								// the speed that the bubble moves
		bubbleCreation = GetComponentInParent<BubbleCreation> ();
		bubblePosition = GetComponent<Transform> ().position; 
		bubbleRigid = GetComponent<Rigidbody> ();
		min = bubbleCreation.min;									// min & max: the borders of the room aka. the space that the bubbles can move in
		max = bubbleCreation.max;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		MoveBubblesRandom();
	}
		
	void MoveBubblesRandom() {
		// if the bubble is still within the boundaries, move it
		if ((bubblePosition.x > min.x) && (bubblePosition.x < max.x)
		    && (bubblePosition.y > min.y) && (bubblePosition.y < max.y)
		    && (bubblePosition.z > min.z) && (bubblePosition.z < max.z)) {

			bubbleRigid.AddForce (direction * Time.fixedDeltaTime * speed);

			// clamp the acceleration to maxVelocity.
			if (Mathf.Abs (bubbleRigid.velocity.x) > maxVelocity) {
				bubbleRigid.velocity = new Vector3 (Mathf.Sign (bubbleRigid.velocity.x) * maxVelocity, bubbleRigid.velocity.y, bubbleRigid.velocity.z);
			}
			if (Mathf.Abs (bubbleRigid.velocity.y) > maxVelocity) {
				bubbleRigid.velocity = new Vector3 (bubbleRigid.velocity.x, Mathf.Sign (bubbleRigid.velocity.y) * maxVelocity, bubbleRigid.velocity.z);
			}
			if (Mathf.Abs (bubbleRigid.velocity.z) > maxVelocity) {
				bubbleRigid.velocity = new Vector3 (bubbleRigid.velocity.x, bubbleRigid.velocity.y, Mathf.Sign (bubbleRigid.velocity.z) * maxVelocity);
			}


			bubblePosition = transform.position;	// update the position of the bubble
		} else {			// else correct its position and give it another direction (bouncing on the wall)
			if (bubblePosition.x <= min.x) {
				positionCorrection = new Vector3 (transform.position.x + space, transform.position.y, transform.position.z);
			} else if (bubblePosition.x >= max.x) {
				positionCorrection = new Vector3 (transform.position.x - space, transform.position.y, transform.position.z);
			} else if (bubblePosition.y <= min.y) {		// if outside of bottom barrier
				positionCorrection = new Vector3 (transform.position.x, transform.position.y + space, transform.position.z); // move it a bit upwards & change direction
			} else if (bubblePosition.y >= max.y) {		// if outside of top barrier
				positionCorrection = new Vector3 (transform.position.x, transform.position.y - space, transform.position.z); // move it a bit downwards & change direction
			} else if (bubblePosition.z <= min.z) {		
				positionCorrection = new Vector3 (transform.position.x, transform.position.y, transform.position.z + space); // move it a bit upwards & change direction
			} else if (bubblePosition.z >= max.z) {		
				positionCorrection = new Vector3 (transform.position.x, transform.position.y, transform.position.z - space); // move it a bit downwards & change direction
			}
				
			direction = GetMovementStyle ();					// get a pseudorandom direction vector
			bubbleRigid.MovePosition (positionCorrection);		// correct the position of the bubble (move just a little bit)
			bubbleRigid.velocity = Vector3.zero;								// reset the velocity
			bubbleRigid.AddForce (direction * Time.fixedDeltaTime * speed);		// start moving the bubble again
		
			bubblePosition = transform.position;
		}
	}

	Vector3 GetMovementStyle() {
		List<int> combination = new List<int>();			// a list with the combination of numbers for the vector: -1, 0 or 1
		do {
			combination.Clear();
			for (int i = 0; i < 3; i++) {
				combination.Add (RollNumber ());
			}
		} while (combination.IndexOf (0) != combination.LastIndexOf (0));		// make sure that there are no more than one 0s, to avoid vertical/horizontal move
		return new Vector3 (combination [0], combination [1], combination [2]);

	}

	int RollNumber() {							// a random int -1, 0 or 1
		return (int)Random.Range (-1, 2);
	}

	void OnTriggerEnter(Collider co) {			// TODO: DELETE?
		if (co.tag.Equals ("Player")) {
			this.GetComponent<Renderer> ().material.color = Color.red;			// TODO: DELETE.
			Debug.Log ("player collided!");
			StartCoroutine ("WaitAndDestroy");
		}

		if (co.tag.Equals ("Muscle")) {
			Debug.Log ("muscle collided!");
			this.GetComponent<Renderer> ().material.color = Color.blue;			// TODO: DELETE.

			StartCoroutine ("WaitAndDestroy");
		}
	}

	IEnumerator WaitAndDestroy() {
		yield return new WaitForSeconds (0.5f);
		//audioSource.Play ();
		Destroy (gameObject);
	}
}
