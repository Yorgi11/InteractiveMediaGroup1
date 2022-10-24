using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCTRL : MonoBehaviour
{
    [SerializeField] private float MaxCamDist;
    [SerializeField] private float speed;

    private float dist;

    private Vector3 dir;
    private Vector3 offset = new Vector3(0, 0, -10f);

    private Player player;
    private Rigidbody2D rb;
    void Start()
    {
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(player.transform.position + offset, transform.position);
        dir = ((player.transform.position + offset) - transform.position).normalized;
        if (dist >= MaxCamDist) rb.velocity = dir * player.GetSpeed();
        if ((Mathf.Abs(player.transform.position.x - transform.position.x) <= 0.1f || Mathf.Abs(player.transform.position.y - transform.position.y) <= 0.1f) && dist < MaxCamDist) rb.velocity = Vector2.zero;
    }
}
