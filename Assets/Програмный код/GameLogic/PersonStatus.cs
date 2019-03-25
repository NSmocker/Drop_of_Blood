using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PersonStatus : MonoBehaviour
{

   public gameCategory category;
   public gameSide side;

   public rangeKind kind;


    public int hp_current, hp_max, mp_max, mp_current;// Показатели ХП и маны.
    public int damage;//Дамаг
    public int move_speed;// Скорость бега. 


    public bool isDead;
    public bool isCreep;
    public bool isMoving;
    public bool isAttacking;
    public bool isGrounded;
    public bool isJumping;

    
 
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (hp_current <= 0) isDead = true;
        
    }
}
