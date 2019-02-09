using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonCreepMelleController : MonoBehaviour
{
    PersonStatus status;
    public GameObject target;
    public Transform []path;

    public int current_weypoint=0;
    public float distance_to_point;
    // Start is called before the first frame update
    void Start()
    {
        current_weypoint = 0;
        status = GetComponent<PersonStatus>();
    }

    public void GoToWeyPoint()
    {
        //to do //Test for UnityGitHub

    }


    // Update is called once per frame
    void Update()
    {

        if (!target)
        {
            if (current_weypoint != path.Length)
            {
                distance_to_point = Vector3.Distance(transform.position,path[current_weypoint].position);
                if (distance_to_point <= 1) { current_weypoint++; } else { GoToWeyPoint(); }
                //my test


            }
            //TODO go to path points
        }
        else
        {
            //TODO KILLLL!!!!
        }
    }
}
