using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : Platform
{
 
    void OnCollisionEnter2D(Collision2D other)
    {
        base.OnCollisionEnter2D(other);
        transform.parent.GetComponent<Rigidbody2D>().isKinematic = false;
    }

}
