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
    [SerializeField] private float targetExtend;

    [SerializeField] private float bulletForce;
    [SerializeField] private float shootDelay;

    [SerializeField] private GameObject bulletSpawn;
    [SerializeField] private Bullet bullet;

    private float currentHp;
    private float dist;
    private float maxSpeed;

    private Vector2 dir;

    private bool chasePlayer;
    private bool attackPlayer;

    private Move move;
    private Shoot Gun;
    private Player player;
    private Manager manager;

    private bool timerRun = false;
    private float timerMax = 4;
    private float timerCount;
    private Vector3 playerLast;

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

        timerCount = timerMax;
    }
    void Update()
    {
        // direction from the enemy to the player
        dir = (player.transform.position - transform.position).normalized;
        // distance between the enemy and the player
        dist = Vector2.Distance(player.transform.position, transform.position);
        // if in targetrange chase the player
        if (dist <= targetRange) chasePlayer = true;
        else chasePlayer = false;

        transform.up = dir;

        if (!meleeOnly)
        {
            // if in attack range attack the player
            // ranged enemy only
            if (dist <= attackRange) attackPlayer = true;
            else attackPlayer = false;
        }
        move.SpeedLimitXY(speed, rb);
    }
    private void FixedUpdate()
    {
        // if in range chase the player
        if (chasePlayer)
        {
            if (!attackPlayer) speed = maxSpeed;
            move.MoveXYVelocity(transform.up.x, transform.up.y, speed, rb);
            timerRun = false;
            timerCount = timerMax;
        }
        else if(dist > targetRange && dist <= targetRange + targetExtend && timerCount == timerMax)
        {
            //runs upon player leaving targetRange, resets when player next enters targetRange
            playerLast = transform.up; //save where the player last was
            timerRun = true;
        }

        // if attacking half the speed
        if (attackPlayer)
        {
            speed = maxSpeed * 0.75f;
            if (Gun.GetCanShoot()) Gun.ShootXY(bulletForce, shootDelay, bullet, bulletSpawn);
        }

        if (timerRun)
        {
            Vector2 lastDir = (playerLast -transform.position).normalized;
            move.MoveXYVelocity(lastDir.x, lastDir.y, (speed * 0.9f), rb); //move to the player last position at a reduced speed
            timerCount -= Time.smoothDeltaTime;
            if (timerCount <= 0) timerRun = false; chasePlayer = false;
        }

        //slowly follow the player
        if (dist > targetRange + targetExtend) {
            speed = maxSpeed * 0.4f;
            move.MoveXYVelocity(transform.up.x, transform.up.y, speed, rb);
        }
        
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
