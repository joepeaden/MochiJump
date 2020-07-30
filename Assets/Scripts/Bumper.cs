using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : EnvironmentObject
{
	public Vector3 fieldEntryPoint;

    protected void Start()
    {
		base.Start();
		type = EOType.Bumper;
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			other.gameObject.GetComponent<PlayerBouncer>()?.Bounce(fieldEntryPoint, true);

			// if already touched, don't want to count as new platform touched or increase boost meter
			if (!touched)
			{
				PlayerBoost playerBoost = other.gameObject.GetComponent<PlayerBoost>();
				if (playerBoost != null)
				{
					playerBoost.BoostMeter += boostValue;
				}

				UpdateEOTouched(pointValue);
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
