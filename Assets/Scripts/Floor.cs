using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MovingObject
{
	private float speed = 1;
    private Transform playerTf;
    private float catchupHeight = 10;
    private Transform cameraTf;

    public float increment = 0.01f;

	void Start()
	{
		SetStartPosition(transform.position);

        playerTf = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        cameraTf = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
    }

    // TOO hot to handle! I make more noise than HEAVY METAL!
    // WUUUU TANGGG
 
	void Update()
    {
        // NOTE: needs to be improved
        // catchup height plus the center of the camera position
        float height = catchupHeight + cameraTf.position.y;

        // if player is above height, catch up with player (this should move the camera as well)
        if (playerTf.position.y > height)
            transform.Translate(new Vector3(0f, 5f * Time.deltaTime, 0f)); 

		//speed += increment;
		transform.Translate(0, speed * Time.deltaTime, 0);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		// if we hit the player, reset the game
		if(other.tag == "Player")
		{
			other.gameObject.GetComponent<Player>().GameOver();
		}
        
        // clean up old platforms as they are passed
        if(other.tag == "Platform")
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
