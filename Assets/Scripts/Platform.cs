using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
	private float thrust = 400f;

	protected void OnCollisionEnter2D(Collision2D other)
	{
		other.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * thrust);
	}

}
