using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Floor : MovingObject
{
    // for debugging purposes
    [Header("Debugging")]
    [SerializeField]
    private bool move = true;
    [SerializeField]
    private bool accelerate = true;

    [Header("Speed Variables")]
    // how fast camera catches up when player is ahead
    [SerializeField]
    private float catchupSpeed;

    [SerializeField]
    private float speed = 1.5f;

    [SerializeField]
    private float floorAcceleration = 0.001f;

    private Transform playerTf;
    private float catchupHeight = 10;
    private Transform cameraTf;

	void Start()
	{
		SetStartPosition(transform.position);

        playerTf = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        cameraTf = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
    }

    // call me the rap ASASSINATOR!
    // WUUUU TANGGG
 
	void Update()
    {
        // move will be off if debugging
        if (move)
        {
            // catchup height plus the center of the camera position
            float height = catchupHeight + cameraTf.position.y;

            // if player is above height, catch up with player (this should move the camera as well)
            // NOTE: the speed could be calculated by the max player velocity, from the force applied by each platform
            //         ... more likely, will want to just make it follow the player directly
            if (playerTf.position.y > height)
                transform.Translate(new Vector3(0f, catchupSpeed * Time.deltaTime, 0f));

            UpdateSpeed();
            transform.Translate(0, speed * Time.deltaTime, 0);
        }
    }

    private void UpdateSpeed()
    {
        // good enough for now!
        if (accelerate)
        {
            floorAcceleration = 0.001f;
            speed += floorAcceleration;

        }
    }

	private void OnTriggerEnter2D(Collider2D other)
	{
		// if we hit the player, reset the game
		if(other.tag == "Player")
		{
			other.gameObject.GetComponent<Player>().GameOver();
		}
        
        // clean up old platforms as they are passed
        if(other.tag == "Platform" || other.tag == "BoostEmitter")
        {
            Destroy(other.gameObject);
        }
	}

    // set the height for which the floor must catch up with the player (& thus the camera)
    // based on orthosize of camera
    public void SetCatchupHeight(float orthoSize)
    {
        catchupHeight = orthoSize / 3;
    }

	public override void Reset()
	{
		base.Reset();

		// reset floor speed
		speed = 1;
	}
}
