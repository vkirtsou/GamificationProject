using UnityEngine;
using System.Collections;

public class DiamondCreation : MonoBehaviour {


	Vector3 min = new Vector3(0f, 0f, 0f);		// should be changed to fit the room
	Vector3 max = new Vector3(50f, 10f, 50f); // should be changed to fit the room
	GameObject diamonds;

	// Use this for initialization
	void Awake() {
		diamonds = new GameObject ("Diamonds");
	}
	void Start () {		
		CreateDiamonds ();
	}
		
	void Update() {
		RotateDiamondsRandom ();	
	}

	void CreateDiamonds() {
		for (int i = 0; i < 2000; i++) {
			GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);	// creates a cube
			cube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);			// make it smaller
			//cube.transform.rotation = new Quaternion (0f, 0f, 0f, 45f);		// rotate to look like a diamond
			Vector3 cubePos = CreateRandomVector3();
			cube.transform.position = cubePos;									// position it randomly
			cube.transform.parent = diamonds.transform;							// parent the cube/diamond
		}
	}

	Vector3 CreateRandomVector3() {
		Vector3 randomXYZ;
		float randSpace;
		float randomX;
		float randomY;
		float randomZ;

		randomX = Random.Range (min.x, max.x);
		randomY = Random.Range (min.y, max.y);
		randomZ = Random.Range (min.z, max.z);
		randomXYZ = new Vector3 (randomX, randomY, randomZ);

		return randomXYZ;
	}

	void RotateDiamondsRandom() {			// random movement, has a trembling effect. Problem: initial plan was to choose one rotation randomly for each cube,
		int size = diamonds.GetComponentInChildren<Transform>().childCount;				// but it does it repeatedly and it changes in every frame, as it's called repeatedly
		for (int i = 0; i < size; i++) {
			Transform diamond = diamonds.GetComponentInChildren<Transform> ().GetChild (i);
			int rotationType = (int)Random.Range(0, 4);
			float speed = Random.Range (15, 35);

			switch (rotationType) {
			case 0:
				diamond.transform.Rotate (Vector3.right, Time.deltaTime * speed);
				break;
			case 1:
				diamond.transform.Rotate (Vector3.up, Time.deltaTime * speed);
				break;
			case 2:
				diamond.transform.Rotate (Vector3.down, Time.deltaTime * speed);
				break;
			case 3:
				diamond.transform.Rotate (Vector3.left, Time.deltaTime * speed);
				break;
			default:
				Debug.Log ("oops");
				break;
			}
		}
	}

	void RotateDiamondsSimple() {
		int size = diamonds.GetComponentInChildren<Transform>().childCount;
		float speed = Random.Range (15, 35);

		for (int i = 0; i < size; i++) {
			Transform diamond = diamonds.GetComponentInChildren<Transform> ().GetChild (i);
			diamond.transform.Rotate (Vector3.up, Time.deltaTime * speed);			// it's actually turning right
		}
	}
}
