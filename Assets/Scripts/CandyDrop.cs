using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyDrop : MonoBehaviour
{
    public int NumberOfCandy;

    public void DropCandy()
    {
        for (int i = 0; i < NumberOfCandy; i++)
        {
            float x = Random.Range(-1.4f, 1.4f);
            float y = Random.Range(-0.8f, 0.8f);
            Instantiate(MainGameplay.Instance.Candy, new Vector2(transform.position.x + x, transform.position.y + y), Quaternion.identity);
        }
    }
}
