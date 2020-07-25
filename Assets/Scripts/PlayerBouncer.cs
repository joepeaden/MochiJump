using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBouncer : MonoBehaviour
{
    // may want to have max thrust
    private const float MAX_BOUNCE_THRUST = 100000f;

    // would be const but doesn't show up in editor
    [SerializeField]
    public float ORIGINAL_THRUST_VALUE;
    
    [SerializeField]
    private float bounceThrust;
    public float BounceThrust
    {
        get => bounceThrust;
        set
        {
            if (value <= MAX_BOUNCE_THRUST)
            {
                bounceThrust = value;
            }
        }
    }

    private Rigidbody2D rb;

    public void Bounce(Vector3 fieldEntryPoint, bool vertical = false)
    {
        // avoid compounding speed increases
        //rb.velocity = Vector2.zero;

        // covering platforms for now
        if (fieldEntryPoint == Vector3.zero)
        {
            GetComponent<Rigidbody2D>().AddForce(transform.up * bounceThrust);
            return;
        }

        Vector3 forceDirection;
        // if came from right side
        if(transform.position.x < fieldEntryPoint.x)
        {
            // apply 45 degree angle force 
            forceDirection = new Vector3(1, 1, 0);
        }
        else
        {
            // apply 315 dgree angle
            forceDirection = new Vector3(-1, 1, 0);
        }


        rb.AddForce(forceDirection * bounceThrust);

        // creating vector for reflection
        //Vector3 fieldEntryVector = transform.position - fieldEntryPoint;
        //Vector3.Normalize(fieldEntryVector);

        //Vector3 normal = vertical ? Vector3.right : Vector3.up;

        //Vector3 reflectedVector = Vector3.Reflect(fieldEntryVector, normal); 


        //rb.AddForce(reflectedVector * bounceThrust);
        
    }

    private void Start()
    {
        bounceThrust = ORIGINAL_THRUST_VALUE;

        rb = GetComponent<Rigidbody2D>();
    }

    public void Reset()
    {
        bounceThrust = ORIGINAL_THRUST_VALUE;
    }
}
