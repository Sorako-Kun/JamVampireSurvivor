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
    public float CoolDown = 2;
    public bool QuarterShoot = false;

    [Header("Movement")]
    public float Speed = 5;
    private float _timerCoolDown;

    [Header("Movement")]
    public int CurrentCandy;
    public int MaxCandy;
    public Text CandyText; 

    private Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        CurrentLife = MaxLife;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shoot();
        //if (QuarterShoot)
        //    ShootQuarter();
        HealthBarUpdate();

        // Update Candies Number
        CandyText.text = "Candies: " + CurrentCandy.ToString() + "/" + MaxCandy.ToString();
    }

    private void Shoot()
    {
        if (MainGameplay.Instance.Enemies.Count > 0)
        {
            _timerCoolDown += Time.deltaTime;

            if (_timerCoolDown < CoolDown)
                return;

            _timerCoolDown -= CoolDown;
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

    private void ShootQuarter()
    {
        if (MainGameplay.Instance.Enemies.Count > 0)
        {
            _timerCoolDown += Time.deltaTime;

            if (_timerCoolDown < CoolDown)
                return;

            _timerCoolDown -= CoolDown;
            GameObject go = Instantiate(PrefabBullet, transform.position, Quaternion.identity);

            // Normal Shoot
            Vector3 direction = transform.position - transform.position;
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
