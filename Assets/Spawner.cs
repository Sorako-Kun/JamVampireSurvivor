using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> Enemies;
    public float SpawnTime;
    public int NumberSpawnMin;
    public int NumberSpawnMax;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Spawn(); 
    }
    public void Spawn()
    {
        timer += Time.deltaTime;
        if(timer > SpawnTime)
        {
            int rand = Random.Range(NumberSpawnMin, NumberSpawnMax);
            for (int i = 0; i < Enemies.Count; i++)
            {
                int rand2 = Random.Range(0, rand);
                rand -= rand2;
                for (int j = 0; j < rand2; j++)
                {
                    float x = Random.Range(-1.4f, 1.4f);
                    float y = Random.Range(-0.8f, 0.8f);
                    GameObject spawn = Instantiate(Enemies[i], new Vector2(transform.position.x + x, transform.position.y + y), Quaternion.identity);
                    MainGameplay.Instance.Enemies.Add(spawn.GetComponent<EnemyController>());
                }
            }
            timer -= SpawnTime;
        }
    }
}
