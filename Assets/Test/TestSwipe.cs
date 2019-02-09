using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class TestSwipe : MonoBehaviour {
	
	public GameObject toGenerate;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		Debug.Log (CrossPlatformInputManager.GetAxis ("Melle"));
		if (CrossPlatformInputManager.GetAxis ("Melle")>0)
		{

			
			Destroy(Instantiate (toGenerate, transform.position, transform.rotation,transform),30f);
		}

	}
}
