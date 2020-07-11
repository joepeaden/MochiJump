using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceUpPlatform : Platform
{
    [SerializeField]
    private float thrustIncreaseMultiplier;

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        base.OnCollisionEnter2D(other);

        PlayerBouncer bouncer = other.gameObject.GetComponent<PlayerBouncer>();
        bouncer.BounceThrust += bouncer.BounceThrust * thrustIncreaseMultiplier;
    }


}
