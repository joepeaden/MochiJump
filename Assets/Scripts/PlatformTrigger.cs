using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrigger : MovingObject
{
    private LevelGenerator levelGenerator;

    void Start()
    {
        SetStartPosition(transform.position);

        levelGenerator = GameObject.FindGameObjectWithTag("LevelGenerator").GetComponent<LevelGenerator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            LevelGenerator.GeneratePlatform(false);

        transform.Translate(new Vector3(0f, 5f, 0f));
    }
}
