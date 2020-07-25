﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
	[SerializeField]
	protected int boostValue = 1;

	// delegate for updating level generator when platform is touched
	public delegate void Callback();
	public Callback UpdatePlatformsTouched;

	private bool touched;

	private GameObject parentGO;

    private void Start()
    {
		// set up this way cause need platform to be solid but not bounce players off sides, for example
		parentGO = transform.parent.gameObject;
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
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

			UpdatePlatformsTouched();
			touched = true;

			PingFeedback();
		}
	}

	private void PingFeedback()
    {
		parentGO.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);

    }

}
