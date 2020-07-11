using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour
{
	private void OnTriggerExit2D(Collider2D other)
	{
		// if player leaves bounds of camera, transport to other side (screen wrap)
		if(other.gameObject.tag == "Player")
		{
			TeleportPlayer(other.gameObject);
			return;
		}

		// if a moving platform. Can't use tag because tag must be Platform for floor to delete
		MovingPlatform mp = other.gameObject.GetComponent<MovingPlatform>();
		if (mp)
		{
			mp.SwitchDirections();
			return;
		}
	}

	private void TeleportPlayer(GameObject playerObject)
    {
		BoxCollider2D box = GetComponent<BoxCollider2D>();
		float posx = (box.size.x / 2) + 1;

		Transform tf = playerObject.gameObject.transform;

		bool aboveYBounds = tf.position.y >= (transform.position.y + box.size.y / 2);
		bool belowYBounds = tf.position.y <= (transform.position.y - box.size.y / 2);

		// no action if exiting top or bottom of bounds (for now)
		if (aboveYBounds || belowYBounds)
			return;

		// if exiting on right side, teleport to left side
		if (tf.position.x > 0)
			tf.position = new Vector3(-posx, tf.position.y, tf.position.z);
		else
			tf.position = new Vector3(posx, tf.position.y, tf.position.z);

	}
}
