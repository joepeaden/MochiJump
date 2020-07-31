using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// I really don't think doing it this way was necessary. Oh well, guess I was trying to learn

public interface Command
{
	void Execute(GameObject gameObject);
}

public class GoLeftCommand : MonoBehaviour, Command
{
	public float moveForce;

	public void Execute(GameObject gameObject)
	{
		Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
		Transform tf = gameObject.GetComponent<Transform>();

		if(rb != null && tf != null)
		{
			//tf.Translate(-10 * Time.deltaTime, 0, 0);
			rb.AddForce(-tf.right * moveForce);
		}
		else
			Debug.LogError("GoLeftCommand: rb or tf not found");
				
	}
}

public class GoRightCommand : Command
{
	public float moveForce;
	public void Execute(GameObject gameObject)
	{
		Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
		Transform tf = gameObject.GetComponent<Transform>();

		if(rb != null && tf != null)
		{
			//tf.Translate(10 * Time.deltaTime, 0, 0);
			rb.AddForce(tf.right * moveForce);
		}
		else
			Debug.LogError("GoRightCommand: rb or tf not found");

	}
}