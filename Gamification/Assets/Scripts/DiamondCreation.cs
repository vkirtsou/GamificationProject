using UnityEngine;
using System.Collections;

public class DiamondCreation : MonoBehaviour {

	public int numberOfGems = 100;

	private Vector3 min = new Vector3(0f, 0f, 0f);		// should be changed to fit the room
	private Vector3 max = new Vector3(6f, 4f, 5f); 		// should be changed to fit the room
	private GameObject diamonds;

	// Use this for initialization
	void Awake() {
		diamonds = new GameObject ("Diamonds");
	}
	void Start () {		
		CreateDiamonds ();
	}

	void CreateDiamonds() {
		for (int i = 0; i < numberOfGems; i++) {
			GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);	// creates a cube
			cube.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);		// make it smaller
			Vector3 cubePos = CreateRandomVector3();
			cube.transform.position = cubePos;									// position it randomly
			cube.transform.parent = diamonds.transform;							// parent the cube/diamond
			cube.tag = "Diamond";												// tag it as "Diamond"
			cube.AddComponent(typeof(DiamondBehavior));							// attach the diamond script
			cube.GetComponent<BoxCollider> ().isTrigger = true;					// make it trigger
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
}
