using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MovingObject
{
	private float speed = 1;
	public float increment = 0.01f;

	void Start()
	{
		SetStartPosition(transform.position);
	}

	void Update()
	{
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
	}

	public override void Reset()
	{
		base.Reset();

		// reset floor speed
		speed = 1;
	}
}
