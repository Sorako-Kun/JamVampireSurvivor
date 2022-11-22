using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    public Vector2 aim;
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        float Horizontal = Input.GetAxis("HorizontalJR");
        float Vertical = Input.GetAxis("VerticalJR");
        if (Horizontal != 0 || Vertical != 0)
        {
            aim = new Vector2(Horizontal, Vertical);
            if (aim.sqrMagnitude > 0)
            {
                aim.Normalize();
                transform.localPosition = aim;
            }
        }
    }
}
