using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBox : MonoBehaviour
{
    public GameObject TargetObject;
    public int DisappearTime = 5;
    public float HorizontalPadding;
    public float VerticalPadding;


    private void Start()
    {
        transform.position = new Vector2(TargetObject.transform.position.x + HorizontalPadding, TargetObject.transform.position.y + VerticalPadding);
        Destroy(gameObject, DisappearTime);
    }
}
