using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{


    public Animator anim;
    public GameObject cam;
    public GameObject isGroundedCheker;
    public Rigidbody rb;
    public float JumpForce, moveSpeed, radius;
    public bool collided, grounded;
    public int motion;
   


    public float Rotation_Smoothness; //Believe it or not, adjusting this before anything else is the best way to go.
    private float Resulting_Value_from_Input;
    private Quaternion Quaternion_Rotate_From;
    private Quaternion Quaternion_Rotate_To;
    
    // Use this for initialization
    void Start()
    {
      
        radius = GetComponent<CapsuleCollider>().radius;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
            Vector3 NextDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            if (NextDir != Vector3.zero)
            {
                Quaternion_Rotate_From = transform.rotation;
                var lol = Quaternion.LookRotation(cam.transform.TransformDirection(NextDir));
                var eLol = lol.eulerAngles;
                eLol.x = 0;
                eLol.z = 0;

                Quaternion_Rotate_To = Quaternion.Euler(eLol);
                transform.rotation = Quaternion.Lerp(Quaternion_Rotate_From, Quaternion_Rotate_To, Time.deltaTime * Rotation_Smoothness);
            }
    }


    Vector3 GetRotationFromCamera()
    {

        var thisRotation = transform.localEulerAngles;
        thisRotation.y = cam.transform.localEulerAngles.y;
        return thisRotation;
    }
    bool GroundCheck()
    {
        RaycastHit hit;
        var temp = Physics.SphereCast(isGroundedCheker.transform.position,radius, -transform.up, out hit, radius);
        try { /*Debug.Log(hit.collider.name); */}
        catch { }
        return temp;
    }

 
    public void HeroMove()
    {
        var h = Input.GetAxis("Horizontal") ;
        var v = Input.GetAxis("Vertical") ;

        if (h != 0 || v != 0)
        {
            motion = 1;
        }
        else
        { motion = 0; }
        
    }


    void FixedUpdate()
    {

        grounded = GroundCheck();
        anim.SetBool("Landed", grounded);
        HeroMove();


        if (grounded==true)
        {
            anim.SetBool("Jump", false);
            anim.SetInteger("isMoving", motion);
            rb.velocity = transform.TransformDirection(new Vector3(0, rb.velocity.y, motion * moveSpeed));
            if (Input.GetButton("Jump"))
            {
               rb.AddRelativeForce((Vector3.up * JumpForce*100));
                anim.SetBool("Jump", true);
            }
        }
      
        if (grounded == false)
          {
            anim.SetInteger("isMoving", 0);

            rb.velocity = transform.TransformDirection(new Vector3(0, rb.velocity.y, (motion * moveSpeed)/2));
            if (collided)
            {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
            }

        }
           
        







    }

}