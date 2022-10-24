using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private bool meleeOnly;

    [SerializeField] private float maxHp;
    [SerializeField] private float speed;
    [SerializeField] private float meleeDmg;
    [SerializeField] private float rangeDmg;
    [SerializeField] private float targetRange;
    [SerializeField] private float attackRange;

    [SerializeField] private float bulletForce;
    [SerializeField] private float shootDelay;

    [SerializeField] private GameObject bulletSpawn;
    [SerializeField] private Bullet bullet;

    private float currentHp;
    private float range;
    private float maxSpeed;

    private Vector2 dir;

    private bool followPlayer;
    private bool attackPlayer;

    private Move move;
    private Shoot Gun;
    private Player player;
    private Manager manager;

    private Rigidbody2D rb;
    void Start()
    {
        player = FindObjectOfType<Player>();
        manager = FindObjectOfType<Manager>();
        move = GetComponent<Move>();
        Gun = GetComponent<Shoot>();
        rb = GetComponent<Rigidbody2D>();
        currentHp = maxHp;
        maxSpeed = speed;
    }
    void Update()
    {
        // direction from the enemy to the player
        dir = (player.transform.position - transform.position).normalized;
        // distance between the enemy and the player
        range = Vector2.Distance(player.transform.position, transform.position);
        // if in targetrange follow the player
        if (range <= targetRange) followPlayer = true;
        else followPlayer = false;

        transform.up = dir;

        if (!meleeOnly)
        {
            // if in attack range attack the player
            // ranged enemy only
            if (range <= attackRange) attackPlayer = true;
            else attackPlayer = false;
        }
        move.SpeedLimitXY(speed, rb);
    }
    private void FixedUpdate()
    {
        // if in range follow the player
        if (followPlayer) move.MoveXYVelocity(transform.up.x, transform.up.y, speed, rb);
        else move.Stop2D(rb);

        // if attacking half the speed
        if (attackPlayer)
        {
            speed = maxSpeed * 0.75f;
            if (Gun.GetCanShoot()) Gun.ShootXY(bulletForce, shootDelay, bullet, bulletSpawn);
        }
        else speed = maxSpeed;
    }
    public void TakeDamage(float dmg)
    {
        currentHp -= dmg;
        if (currentHp <= 0) Die();
    }

    private void Die()
    {
        manager.RemoveEnemy();
        Destroy(gameObject);
        Debug.Log("Enemy Died");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == 6) player.TakeDamage(meleeDmg);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == 6) player.TakeDamage(meleeDmg * Time.deltaTime);
    }
}
