using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : EnvironmentObject
{
    protected void Start()
    {
		base.Start();
		type = EOType.Platform;
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "Player")
		{ 
			// for now, just using zero to not make calculations
			other.gameObject.GetComponent<PlayerBouncer>()?.Bounce(Vector3.zero);

			// if already touched, don't want to count as new platform touched or increase boost meter
			if (!touched)
			{
				PlayerBoost playerBoost = other.gameObject.GetComponent<PlayerBoost>();
				if (playerBoost != null)
				{
					playerBoost.BoostMeter += boostValue;
				}

				UpdateEOTouched();
				touched = true;
				PingFeedback(true);
			}
			else
			{
				PingFeedback();
			}
		}
	}
}
