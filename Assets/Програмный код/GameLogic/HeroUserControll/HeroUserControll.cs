using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroUserControll : MonoBehaviour
{
    //Связь с модулем статуса. 
    //Показатели персонажа.
    public int hp_current, hp_max, mp_max, mp_current;// Показатели ХП и маны.
    public int damage;//Дамаг
    public int move_speed;// Скорость бега. 
    /*************************************************/




    /************ Системные статусы *************************/
    
    public bool isDead;
    public bool isMoving;
    public bool isAttacking;
    public bool isGrounded;
    public bool isJumping;
    /*******************************************************/


    public Animator anim;
    public Rigidbody rb;
    public float colliderRadius;
    public float distanceToGround;
    public float JumpForce;




    public GameObject isGroundedCheker;
    public float Rotation_Smoothness;
    private float Resulting_Value_from_Input;
    private Quaternion Quaternion_Rotate_From;
    private Quaternion Quaternion_Rotate_To;


    // Обработка логики склонов

    public float _height;
    public float _heightPadding;
    public LayerMask ground;
    public float maxGroundAngle = 120;
    public bool debug;
    float groundAngle;

    Vector3 forward;
    RaycastHit _hitInfo;







    bool GroundCheck()
    {
        // RaycastHit hit;
        //        var temp = Physics.SphereCast(isGroundedCheker.transform.position, colliderRadius, -transform.up, out hit, distanceToGround);
        var temp = Physics.SphereCast(isGroundedCheker.transform.position, colliderRadius, -transform.up, out _hitInfo, _height - _heightPadding);

        //try { Debug.Log(hit.collider.name); } catch (Exception e) { }
        isGrounded = temp;
        return temp;
    }
    public void UserInput()
    {
        isJumping = Input.GetKey(KeyCode.Space);
        isAttacking = Input.GetKey(KeyCode.Mouse0);
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

        anim.SetBool("isDead", isDead);
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isAttacking", isAttacking);
        GroundCheck();
        UserInput();
        HeroMove();
        HeroRotation();
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isJumping", isJumping);
        

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
            transform.Translate((Vector3.forward * v) / 10, Space.Self);
        }
        if (isGrounded == true)
        {
            if (isMoving) rb.velocity = transform.TransformDirection(Vector3.forward * move_speed);
            if (!isMoving) rb.velocity = transform.TransformDirection(Vector3.forward * 0);
            if (isJumping == true)
            {
                var old = rb.velocity;
                old.y = JumpForce;
                rb.velocity = old;
            }
            else
            {
                transform.position = new Vector3(transform.position.x, _hitInfo.point.y, transform.position.z);
            }
        }
    }

    void Start()
    {
      
        colliderRadius = GetComponent<CapsuleCollider>().radius;
        distanceToGround = GetComponent<CapsuleCollider>().height / 2;
        _height = GetComponent<CapsuleCollider>().height;
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AnimationControll();
    }
}
