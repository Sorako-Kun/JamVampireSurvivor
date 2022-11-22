using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
    }

    [Header("Life")]
    public float MaxLife;
    public float CurrentLife;
    public GameObject HealthBar;

    [Header("Shoot")]
    public GameObject PrefabBullet;
    public float CoolDownWeapon0 = 2;
    public float CoolDownWeapon1 = 2;
    public float CoolDownWeapon2 = 2;
    public float CoolDownWeapon3 = 2;
    public bool QuarterShoot = false;
    public Vector2 aim;
    private Aiming aiming;
    public int index;
    public int maxNumberOfWeapon;

    [Header("Movement")]
    public float Speed = 5;
    private float _timerCoolDown;
    public Camera camera;

    [Header("Candy")]
    public int CurrentCandy;
    public int MaxCandy;
    public Text CandyText; 

    private Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        CurrentLife = MaxLife;
        aiming = GetComponentInChildren<Aiming>();
        camera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        aim = aiming.aim;
        if (Input.GetButtonDown("SwitchRight") && index < maxNumberOfWeapon)
            index++;
        if (Input.GetButtonDown("SwitchLeft") && index > 0)
            index--;

        Move();
        if(index == 0)
            BasicShoot();
        if (index == 1)
            Triple();
        if (index == 2)
            Cross();

        HealthBarUpdate();

        // Update Candies Number
        CandyText.text = "Candies: " + CurrentCandy.ToString() + "/" + MaxCandy.ToString();
    }

    private void Cross()
    {
        if (MainGameplay.Instance.Enemies.Count > 0)
        {
            _timerCoolDown += Time.deltaTime;

            if (_timerCoolDown < CoolDownWeapon1)
                return;

            _timerCoolDown -= CoolDownWeapon1;
            GameObject go = Instantiate(PrefabBullet, new Vector2(transform.position.x, transform.position.y + 0.5f), Quaternion.identity);
            GameObject go1 = Instantiate(PrefabBullet, new Vector2(transform.position.x, transform.position.y - 0.5f), Quaternion.identity);
            GameObject go2 = Instantiate(PrefabBullet, new Vector2(transform.position.x - 0.5f, transform.position.y), Quaternion.identity);
            GameObject go3 = Instantiate(PrefabBullet, new Vector2(transform.position.x + 0.5f, transform.position.y), Quaternion.identity);
            go.GetComponent<Bullet>().Initialize(new Vector2(0, 1));
            go1.GetComponent<Bullet>().Initialize(new Vector2(0, -1));
            go2.GetComponent<Bullet>().Initialize(new Vector2(-1, 0));
            go3.GetComponent<Bullet>().Initialize(new Vector2(1, 0));

        }
    }

    private void Triple()
    {
        if (MainGameplay.Instance.Enemies.Count > 0)
        {
            _timerCoolDown += Time.deltaTime;

            if (_timerCoolDown < CoolDownWeapon1)
                return;

            _timerCoolDown -= CoolDownWeapon1;
            GameObject go = Instantiate(PrefabBullet, aiming.transform.position, Quaternion.identity);
            GameObject go1 = Instantiate(PrefabBullet, aiming.transform.position, Quaternion.identity);
            GameObject go2 = Instantiate(PrefabBullet, aiming.transform.position, Quaternion.identity);
            if (aim.sqrMagnitude > 0)
            {
                aim.Normalize();
                go.GetComponent<Bullet>().Initialize(new Vector2(aim.x + 0.1f, aim.y));
                go1.GetComponent<Bullet>().Initialize(new Vector2(aim.x - 0.4f, aim.y - 0.4f));
                go2.GetComponent<Bullet>().Initialize(new Vector2(aim.x + 0.4f, aim.y + 0.4f));
            }
        }
    }

    private void BasicShoot()
    {
        if (MainGameplay.Instance.Enemies.Count > 0)
        {
            _timerCoolDown += Time.deltaTime;

            if (_timerCoolDown < CoolDownWeapon0)
                return;

            _timerCoolDown -= CoolDownWeapon0;
            GameObject go = Instantiate(PrefabBullet, transform.position, Quaternion.identity);

            EnemyController enemy = MainGameplay.Instance.GetClosestEnemy(transform.position);

            // Normal Shoot
            Vector3 direction = enemy.transform.position - transform.position;
            if (direction.sqrMagnitude > 0)
            {
                direction.Normalize();

                go.GetComponent<Bullet>().Initialize(direction);
            }

        }

    }

    public IEnumerator Knockback(float knockbackDuration, float knockbackPower, Transform obj)
    {
        float timer = 0;
        while(knockbackDuration > timer)
        {
            timer += Time.deltaTime;
            Vector2 direction = (obj.transform.position - transform.position).normalized;
            _rb.AddForce(-direction * knockbackPower);
        }
        yield return 0;
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector2(horizontal, vertical);

        if (direction.sqrMagnitude > 0)
        {
            direction.Normalize();
            transform.position += direction * Speed * Time.deltaTime;
        }
    }
    public void TakeDamage(float damage)
    {
        CurrentLife -= damage;
    }
    public void Heal(float life)
    {
        CurrentLife += life;
    }

    public void HealthBarUpdate()
    {
        HealthBar.transform.localScale = new Vector3(CurrentLife/MaxLife, 1, 0);
        if (CurrentLife <= 0)
        {
            HealthBar.transform.localScale = new Vector3(0, 1, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            if (CurrentCandy < MaxCandy)
            {
                CurrentCandy += 1;
                Destroy(collision.gameObject);
            }
        }      

    }
}
