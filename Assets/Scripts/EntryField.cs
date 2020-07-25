using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryField : MonoBehaviour
{
    // this needs to be changed to include platforms
    // they probably should inherit from some class
    [SerializeField]
    private Bumper bumper;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>())
        {
            bumper.fieldEntryPoint = collision.transform.position;
        }
    }
}
