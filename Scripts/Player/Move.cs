using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public void MoveXY(float horzIn, float vertIn, float speed, float factor)
    {

    }
    public void MoveXYForce(float horzIn, float vertIn, float speed, Rigidbody2D rb)
    {
        rb.velocity += new Vector2(horzIn * speed, vertIn * speed) * 0.5f;
    }
    public void MoveXYVelocity(float horzIn, float vertIn, float speed, Rigidbody2D rb)
    {
        rb.velocity = new Vector2(horzIn * speed, vertIn * speed);
    }
    public void SlowXY(Rigidbody2D rb, float slowRate)
    {
        if (rb.velocity.magnitude <= 0.1f) rb.velocity = Vector2.zero;
        else rb.velocity = rb.velocity.normalized * (rb.velocity.magnitude - slowRate * Time.deltaTime);
    }
    public void MoveXZ(float horzIn, float vertIn, float speed, Rigidbody rb)
    {
        rb.velocity += new Vector3(horzIn * speed * 0.25f, 0f, vertIn * speed * 0.25f);
    }
    public void MoveXZ(Vector3 dir, float speed, Rigidbody rb)
    {
        rb.velocity += new Vector3(dir.x * speed * 0.25f, 0f, dir.z * speed * 0.25f);
    }

    public void SpeedLimitXY(float speed, Rigidbody2D rb)
    {
        if (rb.velocity.x > speed)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else if (rb.velocity.x < -speed)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }

        if (rb.velocity.y > speed)
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
        }
        else if (rb.velocity.y < -speed)
        {
            rb.velocity = new Vector2(rb.velocity.x, -speed);
        }
    }
    public void SpeedLimitXZ(float speed, Rigidbody rb)
    {
        if (rb.velocity.x > speed)
        {
            rb.velocity = new Vector3(speed, rb.velocity.y, rb.velocity.z);
        }
        else if (rb.velocity.x < -speed)
        {
            rb.velocity = new Vector3(-speed, rb.velocity.y, rb.velocity.z);
        }

        if (rb.velocity.z > speed)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed);
        }
        else if (rb.velocity.z < -speed)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -speed);
        }
    }

    public void Stop2D(Rigidbody2D rb)
    {
        rb.velocity = Vector2.zero;
    }
    public void Stop3D(Rigidbody rb)
    {
        rb.velocity = Vector3.zero;
    }
}
