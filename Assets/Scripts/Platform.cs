using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
	protected float thrust = 400f;

	protected virtual void OnCollisionEnter2D(Collision2D other)
	{
		other.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * thrust);
	}

}
