using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShoot : MonoBehaviour {

    public Camera mainCam;
    bool isTaped;
    public GameObject generateTo;

    public float fireRate;

    private float nextFire;
    // Use this for initialization

    void Start () {


    }
	
	void Update ()
    {
		transform.Translate (Input.GetAxis("Horizontal")*Time.deltaTime*10,0,Input.GetAxis("Vertical")*Time.deltaTime*10);
      


      	var viewport_Input = mainCam.ScreenToViewportPoint (Input.mousePosition);





		if (
			(viewport_Input.x > 0 && viewport_Input.x <= 1)
			&&
			(viewport_Input.y > 0 && viewport_Input.y <= 1)) 
		{
			Ray ray = mainCam.ViewportPointToRay(viewport_Input);
			Debug.DrawRay (ray.origin, ray.direction * 25, Color.green);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 200)) {
				//    Debug.Log(hit.collider.name);
				if (Input.GetButton ("Fire1") && Time.time > nextFire) {
					nextFire = Time.time + fireRate;
					Instantiate (generateTo, hit.point, Quaternion.identity);
				}

			}


		}//кінець перевірки на коректність позиції курсора в зоні вьюпорту


	
	} // Кінець Апдейту

}// Кінець класу
