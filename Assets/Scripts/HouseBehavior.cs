using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseBehavior : MonoBehaviour
{
    private GameObject _player;
    public int HouseCandy = 0;
    public Text HouseText;
    public float timer;
    public float SpawnTime = 45f;
    public GameObject NoCandyHouseText;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        // Update Candies Number
        HouseText.text = "Candies at Home: " + HouseCandy.ToString();
        timer += Time.deltaTime;
        if (timer > SpawnTime)
        {
            TextHouseCandies();
            timer -= SpawnTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>())
        {
            if (collision.gameObject.GetComponent<PlayerController>().CurrentCandy > 0) {
                HouseCandy += collision.gameObject.GetComponent<PlayerController>().CurrentCandy;
                collision.gameObject.GetComponent<PlayerController>().CurrentCandy = 0;
                timer = 0;
            }

        }
    }

    public void TextHouseCandies()
    {
        GameObject txt = Instantiate(NoCandyHouseText, NoCandyHouseText.transform.position, Quaternion.identity);
    }

    
}
