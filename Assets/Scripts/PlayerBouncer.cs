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

    public void Bounce()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * bounceThrust);
    }

    private void Start()
    {
        bounceThrust = ORIGINAL_THRUST_VALUE;
    }

    public void Reset()
    {
        bounceThrust = ORIGINAL_THRUST_VALUE;
    }
}
