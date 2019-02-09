using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    public float move_speed;
    
    public float Vertical_Velocity, Jump_Force;
    public CharacterController controller;
    public GameObject VisualBody;
    public Vector3 move_dir_force;
    public Animator kailina_anim;
    // Start is called before the first frame update

    public GameObject Cam;



    Vector3 GetRotationFromCamera()
    {


        var thisRotation = transform.localEulerAngles;
        thisRotation.y = Cam.transform.localEulerAngles.y;
        return thisRotation;
    }
    // Використовувати тільки у Update/FixedUpdate
    void AnimationSeqence()
    {
        Vector3 NextDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (NextDir != Vector3.zero) { kailina_anim.SetBool("isMoving", true); } else { kailina_anim.SetBool("isMoving", false); };

        kailina_anim.speed = 1;

    }


    void RotationSequence()
    {
        Vector3 NextDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    //    NextDir = transform.TransformDirection(NextDir);
        if (NextDir != Vector3.zero) { transform.localEulerAngles = GetRotationFromCamera(); }
        if (NextDir != Vector3.zero) VisualBody.transform.localRotation = Quaternion.LookRotation(NextDir);// VisualBody

    }

    public void MoveSequence()
    {
       


        var move_dir_force = new Vector3(Input.GetAxis("Horizontal")*move_speed*Time.deltaTime,0, Input.GetAxis("Vertical") * move_speed * Time.deltaTime);
        if (controller.isGrounded)
        {
            Vertical_Velocity = -1;
            if(Input.GetButton("Jump")) Vertical_Velocity = Jump_Force*Time.deltaTime;
        }
        else
        {   
            Vertical_Velocity -= 1 * Time.deltaTime;
        }
        move_dir_force.y = Vertical_Velocity;
        move_dir_force = transform.TransformDirection(move_dir_force);
        controller.Move(move_dir_force);

    }

    void Start()
    {
           
      
    }
    private void FixedUpdate()
    {

    }
    // Update is called once per frame
    void Update()
    {
        AnimationSeqence();
        MoveSequence();// должен идти первым
        RotationSequence();
   
    }
}
