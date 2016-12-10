using UnityEngine;
using System.Collections;

public class BubbleCreation : MonoBehaviour {

	public int numberOfBubbles = 100;

	//private Vector3 min = new Vector3(0f, 0f, 0f);		// should be changed to fit the room
	//private Vector3 max = new Vector3(6f, 4f, 5f); 		// should be changed to fit the room
	private GameObject room;
	public Transform roomTransform;
	//private GameObject bubbles;

	public Bounds roomAreaForMoving;				// (not) needed in behavior
	public Vector3 min, max;						// needed in behavior
	public Material bubbleMaterial;
	// Use this for initialization
	void Awake() {
		//bubbles = new GameObject ("Bubbles");
		float posX, posY, posZ, scaleX, scaleY, scaleZ;

		room = GameObject.Find("RoomDimensions");
		roomTransform = GetComponent<Transform> ();

		posX = roomTransform.position.x;
		posY = roomTransform.position.y;
		posZ = roomTransform.position.z;

		scaleX = roomTransform.lossyScale.x;
		scaleY = roomTransform.lossyScale.y;
		scaleZ = roomTransform.lossyScale.z;

		roomAreaForMoving = new Bounds (new Vector3 (posX, posY, posZ), new Vector3 (scaleX, scaleY, scaleZ));
		//roomAreaForMoving.extents = Vector3.zero;

		min = roomAreaForMoving.min;
		max = roomAreaForMoving.max;
	}
	void Start () {
		CreateDiamonds ();
	}

	void CreateDiamonds() {
		for (int i = 0; i < numberOfBubbles; i++) {
			//GameObject bubble = (GameObject)Instantiate (Resources.Load ("Bubble"), bubblePos, Quaternion.identity);

			GameObject bubble = GameObject.CreatePrimitive (PrimitiveType.Sphere);	// creates a cube
			bubble.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);		// make it smaller
			Vector3 bubblePos = CreateRandomVector3();
			bubble.transform.position = bubblePos;									// position it randomly
			bubble.transform.parent = room.transform;							// parent the cube/diamond
			bubble.tag = "Diamond";			// TODO: CHANGE TO BUBBLE??				// tag it as "Diamond"
			bubble.AddComponent(typeof(BubbleBehavior));							// attach the diamond script
			bubble.GetComponent<SphereCollider> ().isTrigger = true;					// make it trigger
			//cube.GetComponent<AudioSource>().pitch = Random.Range (0.5f, 1.5f);
			bubble.AddComponent<Rigidbody>();
			bubble.GetComponent<Rigidbody> ().useGravity = false;
			bubble.GetComponent<Renderer> ().material = bubbleMaterial;
		}
	}

	public Vector3 CreateRandomVector3() {		// needed in behavior
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
