using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepSpawner : MonoBehaviour //  класс который будет спавнить крипов для каждой сторны
{
    public float spawn_timer;
    public float spawn_cooldown; // c какой задержкой будет спавнится пачка крипов.
    public float spawn_delay; // задержка между спавном ботов между друг другом ( для розницы анимации)

    public gameSide side;
    public string line; //  "Лайн будет top или bot"
    public Transform[] wey_points; // Траектория бега для заспавненого крипа. У каждого лайна своя.

    public GameObject[] creeps_to_spawn;// Крипы которые заспавнятся.

    IEnumerator SpawnDelay()
    {

        for (int i = 0; i < creeps_to_spawn.Length; i++) { 
        var pos_offset = new Vector3(i, 0, 0);
        var creep = Instantiate(creeps_to_spawn[i], transform.position + pos_offset, creeps_to_spawn[i].transform.rotation, transform); // заспавненный крип.
        var cr_controller = creep.GetComponent<CreepController>();
        if (wey_points.Length == 0) { Debug.Log("No weypoints!!!"); }
        else { cr_controller.WeyPoints = wey_points;}
        yield return new WaitForSeconds(spawn_delay);
        }
    }

    public void SpawnCreeps()
    {
        StartCoroutine(SpawnDelay());     
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
