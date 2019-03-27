using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreepController : MonoBehaviour
{


    Rigidbody rb;
    PersonStatus status;
    int move_speed;

    public Animator anim;
    public GameObject target;
    public Transform[] WeyPoints;
    public int current_weypoint=0;
    public float distance_to_point;
    public Collider[] objects;
    public GameObject[] Enemys;
    public float[] enemys_distances;




    GameObject nearest_enemy = null;
    float nearEnemy_distance = -100f;


    public void AnimationControll()
    {
        anim.SetBool("isDead", status.isDead);
        anim.SetBool("isMoving", status.isMoving);
        anim.SetBool("isAttacking", status.isAttacking);

    }




    public void ScanEnviro()
    {
        objects = Physics.OverlapSphere(transform.position, 10f);
        foreach(Collider x in objects) // Смотрит ближайшие обьекты в радиусе 10 игровых метром и отбирает те, у которых есть компонент статуса персонажа.
        {
            PersonStatus st = x.gameObject.GetComponent<PersonStatus>();
            if (st != null && !Enemys.Contains(x.gameObject)) { // Если у объекта ЕСТЬ статус, и его нету в списке врагов.
                if(st.side!= status.side) // Если у статуса объекта статус НЕ ТАКОЙ как у нас
                Enemys = Enemys.Concat(new GameObject[] { x.gameObject }).ToArray(); // заносим его в список врагов, и пи**им.
               // enemys_distances = enemys_distances.Concat(new float[] { Vector3.Distance(transform.position, x.gameObject.transform.position)}).ToArray();// не работает, нужно пофиксить.
            }
            st = null;
        }
        foreach (GameObject x in Enemys)
        {
         if (!objects.Contains(x.GetComponent<Collider>()  )) Enemys = Enemys.Where(val => val != x).ToArray();// Если в списке объектов нет колайдера из списка "Врагов". 
         // Мы оставляем все объекты, которые остались в списке коллайдеров.

       

        }
        
        /*
        foreach (GameObject x in Enemys)
        { // Ищем ближайшего из врагов, берем его в таргет и начинаем пинать.

            if (Enemys.Contains(x))
            {
            var dist = Vector3.Distance(transform.position, x.transform.position);
            if (nearEnemy_distance < dist && dist<5){ nearEnemy_distance = dist; nearest_enemy = x;}
            }
        }

    */







    }
    public void RotateToTarget(Transform target_pos)
    {
        var pos = new Vector3(target_pos.position.x, transform.position.y, target_pos.position.z);
        distance_to_point = Vector3.Distance(transform.position, pos); // Смотрим дистанцию
        transform.LookAt(pos);// Перезаписываем высоту и используем её
    }
    public void GoToWeyPoint()
    {
        status.isMoving = true;
        status.isAttacking = false;

        // первым делом, поворачиваем морду к вражине (ну или к точке передвижения)

        RotateToTarget(WeyPoints[current_weypoint]);
        move_speed = status.move_speed;// Смотрим какая у нас скорость движения.
        if (current_weypoint != WeyPoints.Length) // пока мы не на последней точке
        {
            if (distance_to_point <= 1) //  Если дистанция Метр или меньше  
            { current_weypoint++; } // выбираем следующую точку 
            else // Иначе
            {
                rb.velocity = transform.TransformDirection(0, rb.velocity.y, 1 * move_speed); // Бежим в направлении "Напрямую"
                                                                                              // До этого, мы повернулись носом к точке, или врагу. По этому бежать будем к врагу. 
            } // Двигаемся к нужной точке




        }

    }
    public void Attack()
    {
        status.isMoving = false;
        status.isAttacking = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        current_weypoint = 0;
        status = GetComponent<PersonStatus>();
        rb = GetComponent<Rigidbody>();// Библиотека с физикой
        
    }



    // Update is called once per frame
    void Update()
    {
        ScanEnviro();

        if (!target) // Если у  нас в таргете никого нету
        {
            GoToWeyPoint();
        }
        else // Инчае 
        {
            Attack();
        }
    }
}
