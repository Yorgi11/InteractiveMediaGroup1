using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float maxHp;
    [SerializeField] private float speed;

    [SerializeField] private Text HpText;
    [SerializeField] private Text AmmoText;

    private float currentHp;
    private float horzin;
    private float vertin;

    private Rigidbody2D rb;
    private Move move;
    void Start()
    {
        move = GetComponent<Move>();
        rb = GetComponent<Rigidbody2D>();
        currentHp = maxHp;
    }
    void Update()
    {
        horzin = Input.GetAxisRaw("Horizontal");
        vertin = Input.GetAxisRaw("Vertical");

        move.SpeedLimitXY(speed, rb);

        // Text
        HpText.text = "HP: " + currentHp;
    }

    private void FixedUpdate()
    {
        move.MoveXYVelocity(horzin, vertin, speed, rb);
    }
    public void TakeDamage(float dmg)
    {
        currentHp -= dmg;
        if (currentHp <= 0) Die();
    }

    private void Die()
    {
        Debug.Log("Player Died");
    }

    public float GetSpeed()
    {
        return speed;
    }
}
