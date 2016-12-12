using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour {

	private static DontDestroy dontDestroyInstance;

	void Start()
	{
		//Causes UI object not to be destroyed when loading a new scene. If you want it to be destroyed, destroy it manually via script.
		DontDestroyOnLoad(this.gameObject);

		/*(if (dontDestroyInstance == null) {
			dontDestroyInstance = this;
		} else {
			DestroyObject (gameObject);
		}*/
	}

	

}
