using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWork : MovingObject
{
	private GameObject floor;
	private float yOffset;

	void Start()
	{
		floor = GameObject.FindGameObjectWithTag("Floor");

		// offset of camera to floor depends on size of camera, + 5 for player falling off screen
		yOffset = (float) GetComponent<Camera>().orthographicSize + 5;
	}

	void Update()
    {
		if(floor != null)
		{
			float posx = floor.GetComponent<Transform>().position.x;
			// bottom of camera should be alligned with floor
			float posy = floor.GetComponent<Transform>().position.y + yOffset;
			// far back enough to see
			float posz = -30f;

			transform.position = new Vector3(posx, posy, posz);
		}
		else
			Debug.LogError("CameraWork -> Update: floor not found");
	}

	public override void Reset()
	{
		// reset position
		transform.position = new Vector3(0, 0, -10);
	}
}
