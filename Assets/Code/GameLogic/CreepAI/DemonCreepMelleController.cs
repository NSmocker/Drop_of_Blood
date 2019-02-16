using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DemonCreepMelleController : MonoBehaviour
{
    PersonStatus status;
    public GameObject target;
    public Transform []WeyPoints;
    public Rigidbody rb;
    public float move_speed; // скорость движения крипа.


    public int current_weypoint=0;
    public float distance_to_point;

    public Collider[] objects;
    public GameObject[] Persons;

    public void ScanEnviro()
    {
        objects = Physics.OverlapSphere(transform.position, 10f);


        foreach(Collider x in objects)
        {
            PersonStatus st = x.gameObject.GetComponent<PersonStatus>();
            if(st!=null && !Persons.Contains(x.gameObject))Persons = Persons.Concat(new GameObject[]{ x.gameObject}).ToArray();
            st = null;
        }

        foreach (GameObject x in Persons)
        {
         if (!objects.Contains(x.GetComponent<Collider>())) Persons = Persons.Where(val => val != x).ToArray();
        }



    }


    // Start is called before the first frame update
    void Start()
    {
        current_weypoint = 0;
        status = GetComponent<PersonStatus>();
        rb = GetComponent<Rigidbody>();// Библиотека с физикой
    }

    public void RotateToTarget(Transform target_pos)
    {   
        var pos = new Vector3(target_pos.position.x,transform.position.y, target_pos.position.z);
        distance_to_point = Vector3.Distance(transform.position, pos); // Смотрим дистанцию
        transform.LookAt(pos);// Перезаписываем высоту и используем её
    }

    public void GoToWeyPoint() 
    {
        // первым делом, поворачиваем морду к вражине (ну или к точке передвижения)

        RotateToTarget(WeyPoints[current_weypoint]);
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

            //УБИИИИТЬЬЬ!!!!!11!
        }
    }
}
