using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinballLane : MonoBehaviour
{
    public Transform shortBumperTform;

    public Transform longBumperTform;

    public GameObject prevEO;

    private void Start()
    {
        // if prev object is to the right, orient bumpers accordingly
        if (prevEO?.transform.position.x > transform.position.x)
        {
            Vector3 shortPrevPosition = shortBumperTform.position;
            shortBumperTform.position = new Vector3(longBumperTform.position.x, shortBumperTform.position.y, shortBumperTform.position.z);
            longBumperTform.position = new Vector3(shortPrevPosition.x, longBumperTform.position.y, longBumperTform.position.z);
        }
    }
}
