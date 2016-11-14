using UnityEngine;
using System.Collections;

public class ChangeCamera : MonoBehaviour {

	public Camera firstPersonCam;
	public Camera playerCam;
	public Camera faceCam;

	void Start () {
		firstPersonCam.enabled = true;
		playerCam.enabled = false;
		faceCam.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.V)) {
			CameraChange ();
		}
	}

	void CameraChange() {
		firstPersonCam.enabled = !firstPersonCam.enabled;
		playerCam.enabled = !playerCam.enabled;
	}
}
