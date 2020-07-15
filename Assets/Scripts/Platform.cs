using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
	[SerializeField]
	protected int boostValue = 1;

	// delegate for updating level generator when platform is touched
	public delegate void Callback();
	public Callback UpdatePlatformsTouched;

	bool touched;

    protected virtual void OnCollisionEnter2D(Collision2D other)
	{
		other.gameObject.GetComponent<PlayerBouncer>().Bounce();

		// if already touched, don't want to count as new platform touched or increase boost meter
		if (!touched)
		{
			PlayerBoost playerBoost = other.gameObject.GetComponent<PlayerBoost>();
			if (playerBoost != null)
			{
				playerBoost.BoostMeter += boostValue;
			}

			UpdatePlatformsTouched();
			touched = true;
		}
	}

}
