using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isCollidedCheker : MonoBehaviour
{
    public CharController to_send;


    public void OnTriggerEnter()
    {
        to_send.collided = true;
    }
    public void OnTriggerExit()
    {
        to_send.collided = false;
    }
  
}
