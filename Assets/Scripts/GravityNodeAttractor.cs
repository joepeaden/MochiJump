using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script attracts the player with some gravity shiz
// thx brackeys

public class GravityNodeAttractor : MonoBehaviour
{
    private const float GRAVITY_CONSTANT = 6.67F;

    public Rigidbody2D playerRb;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Attract();
    }

    private void Attract()
    {
        Vector3 direction = rb.position - playerRb.position;
        float distance = direction.magnitude;

        float forceMagnitude = GRAVITY_CONSTANT * (rb.mass * playerRb.mass) / Mathf.Pow(distance, 3);
        Vector3 force = direction.normalized * forceMagnitude;

        playerRb.AddForce(force);
    }

}
