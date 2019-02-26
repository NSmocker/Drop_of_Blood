using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PersonStatus : MonoBehaviour
{
    public Animator anim;
    public Rigidbody rb;
    public float colliderRadius;
    public float distanceToGround;
    public float JumpForce;


    public int hp_current,hp_max,mp_max, mp_current;// Показатели ХП и маны.
    public int damage;//Дамаг
    public int move_speed;// Скорость бега. 
    

    public bool isDead;
    public bool isCreep;
    public bool isMoving;
    public bool isAttacking;
    public bool isGrounded;
    public bool isJumping;


    public gameCategory category;
    public gameSide side;
    
    public GameObject isGroundedCheker;

    public float Rotation_Smoothness; 
    private float Resulting_Value_from_Input;
    private Quaternion Quaternion_Rotate_From;
    private Quaternion Quaternion_Rotate_To;







    public void UserInput()
    {
       isJumping = Input.GetKey(KeyCode.Space);
    }



    public void HeroRotation()
    {
        Vector3 NextDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (NextDir != Vector3.zero)//Вращение персонажа.
        {
            Quaternion_Rotate_From = transform.rotation;
            var lol = Quaternion.LookRotation(Camera.main.transform.TransformDirection(NextDir));
            var eLol = lol.eulerAngles;
            eLol.x = 0;
            eLol.z = 0;

            Quaternion_Rotate_To = Quaternion.Euler(eLol);
            transform.rotation = Quaternion.Lerp(Quaternion_Rotate_From, Quaternion_Rotate_To, Time.deltaTime * Rotation_Smoothness);
        }

    }
    public void AnimationControll()
    {
        
        anim.SetBool("isDead",isDead);
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isAttacking", isAttacking);

        if (category == gameCategory.Hero)
        {
            GroundCheck();
            UserInput();
            HeroMove();
            HeroRotation();
            anim.SetBool("isGrounded", isGrounded);
            anim.SetBool("isJumping", isJumping);   
        }

    }
    public void HeroMove()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        if (h != 0 || v != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }


        if (isGrounded == false)
        {
            anim.applyRootMotion = false;
            transform.Translate((Vector3.forward*v)/10, Space.Self);

        }
        if (isGrounded == true)
        {
            anim.applyRootMotion = true;
            if (isJumping == true)
            {
                rb.AddRelativeForce(Vector3.up * JumpForce * 10);
            }
        }
        }
    bool GroundCheck()
    {
        RaycastHit hit;
        var temp = Physics.SphereCast(isGroundedCheker.transform.position, colliderRadius, -transform.up, out hit,distanceToGround);
        //try { Debug.Log(hit.collider.name); } catch (Exception e) { }
        isGrounded = temp;
        return temp;
    }

    void Start()
    {
        colliderRadius = GetComponent<CapsuleCollider>().radius;
        distanceToGround = GetComponent<CapsuleCollider>().height/2;
      
       if(category!=gameCategory.Hero)try { anim = GetComponent<Animator>(); rb = GetComponent<Rigidbody>(); } catch (Exception e) { Debug.Log(e.Message); }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AnimationControll();
      }
}
