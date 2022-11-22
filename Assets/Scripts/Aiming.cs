using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    public Vector2 aim;
    private Transform player;
    private Camera MainCamera;
    private Vector3 MousePos;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerController>().transform;
        MainCamera = player.GetComponent<PlayerController>().camera;
    }

    // Update is called once per frame
    void Update()
    {
        MousePos = MainCamera.ScreenToWorldPoint(Input.mousePosition);
        if (MousePos.x != 0 || MousePos.y != 0)
        {
            aim = MousePos - transform.position;
            float rotZ = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rotZ);

        }
    }
}
