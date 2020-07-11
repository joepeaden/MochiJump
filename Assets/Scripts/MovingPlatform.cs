using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : Platform
{
    // 1 for right, -1 for left
    private int direction = 1;

    [SerializeField]
    private float speed = 5.0f;

    private void Update()
    {
        // this script is attatched to collider, not renderered platform
        transform.parent.Translate(Vector3.right * direction * speed * Time.deltaTime);
    }

    public void SwitchDirections()
    {
        if (direction == 1)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }
    }

}
