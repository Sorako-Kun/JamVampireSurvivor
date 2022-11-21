using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameplay : MonoBehaviour
{
    public static MainGameplay Instance;

    public GameObject Player;
    public GameObject House;
    public List<EnemyController> Enemies;
    public float test;
    private void Awake()
    {
        Instance = this;
        test = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (var enemy in Enemies)
        {
            enemy.Initialize(Player);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public EnemyController GetClosestEnemy(Vector3 position)
    {
        float bestDistance = float.MaxValue;
        EnemyController bestEnemy = null;

        foreach (var enemy in Enemies)
        {
            Vector3 direction = enemy.transform.position - position;

            float distance = direction.sqrMagnitude;

            if ( distance < bestDistance)
            {
                bestDistance = distance;
                bestEnemy = enemy;
            }
        }
        return bestEnemy;
    }
}
