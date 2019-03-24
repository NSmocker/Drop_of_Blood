using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepSpawner : MonoBehaviour //  класс который будет спавнить крипов для каждой сторны
{
    float spawn_timer;
    public float spawn_cooldown; // c какой задержкой будет спавнится пачка крипов.


    public gameSide side;
    public string line; //  "Лайн будет top или bot"
    public Transform[] wey_points; // Траектория бега для заспавненого крипа. У каждого лайна своя.

    public GameObject[] creeps_to_spawn;// Крипы которые заспавнятся.

    public void SpawnCreeps()
    {
        foreach (GameObject x in creeps_to_spawn)
        {
            var creep = Instantiate(x,transform.position,x.transform.rotation,transform); // заспавненный крип.
            var cr_controller = creep.GetComponent<CreepController>(); /// не робит. нужно по читнить
            cr_controller.WeyPoints = wey_points;

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawn_timer >= 0) { spawn_timer -= Time.deltaTime; } 
        else { spawn_timer = spawn_cooldown; SpawnCreeps(); }
    }
}
