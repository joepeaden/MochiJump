using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour
{
	private Vector3 startPosition = new Vector3(0f, 0f, 0f);

	// used for stuff that doesn't start at the origin
	protected void SetStartPosition(Vector3 pos)
	{
		startPosition = pos;
	}

	public virtual void Reset()
	{
		// reset position
		transform.position = startPosition;

		// reset acceleration
		Rigidbody2D rb = GetComponent<Rigidbody2D>();
		if(rb != null)
			rb.velocity = new Vector2(0, 0);
	}
}
