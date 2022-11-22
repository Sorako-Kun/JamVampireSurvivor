using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CandiesCollectedScore : MonoBehaviour
{
    public string CandiesScore;
    public Text CandyText;

    // Start is called before the first frame update
    void Start()
    {
        var _house = GameObject.Find("House");
        CandiesScore = _house.GetComponent<HouseBehavior>().HouseCandy.ToString();
        CandyText.text = "Total Candies: " + CandiesScore.ToString();
    }
}
