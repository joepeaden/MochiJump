using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Command
{
	void Execute(GameObject gameObject);
}

public class GoLeftCommand : MonoBehaviour, Command
{
	public void Execute(GameObject gameObject)
	{
		Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
		Transform tf = gameObject.GetComponent<Transform>();

		if(rb != null && tf != null)
		{
			rb.AddForce(-tf.right * 10);
		}
		else
			Debug.LogError("GoLeftCommand: rb or tf not found");
				
	}
}

public class GoRightCommand : Command
{
	public void Execute(GameObject gameObject)
	{
		Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
		Transform tf = gameObject.GetComponent<Transform>();

		if(rb != null && tf != null)
		{
			rb.AddForce(tf.right * 10);
		}
		else
			Debug.LogError("GoRightCommand: rb or tf not found");

	}
}