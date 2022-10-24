using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletDamage;
    [SerializeField] private int SafeLayer;
    private Vector3 lastpos;
    void Start()
    {
        Destroy(gameObject, 3f);
    }
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer != SafeLayer)
        {
            if (collision.collider.gameObject.layer == 7) collision.collider.gameObject.GetComponentInParent<Enemy>().TakeDamage(bulletDamage);
            if (collision.collider.gameObject.layer == 6) collision.collider.gameObject.GetComponentInParent<Player>().TakeDamage(bulletDamage);
        }
        Destroy(gameObject);
    }
}
