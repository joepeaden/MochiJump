using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
	[SerializeField]
	protected int boostValue = 1;

	// delegate for updating level generator when platform is touched
	//public delegate void Callback();
	//public Callback UpdatePlatformsTouched;

	public Vector3 fieldEntryPoint;

	private bool touched;

	private GameObject parentGO;

	private void Start()
	{
		// set up this way to control what parts of the platform the player bounces off of
		parentGO = transform.parent.gameObject;
	}

	protected virtual void OnCollisionEnter2D(Collision2D other)
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

			//UpdatePlatformsTouched();
			touched = true;

			PingFeedback();
		}
	}

	private void PingFeedback()
	{
		parentGO.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
	}
}
