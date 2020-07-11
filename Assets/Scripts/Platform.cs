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
		//other.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * thrust);
		other.gameObject.GetComponent<PlayerBouncer>().Bounce();

		PlayerBoost playerBoost = other.gameObject.GetComponent<PlayerBoost>();
		if (playerBoost != null)
        {
			playerBoost.BoostMeter += boostValue;
        }

		if (!touched)
		{
			UpdatePlatformsTouched();
			touched = true;
		}
	}

}
