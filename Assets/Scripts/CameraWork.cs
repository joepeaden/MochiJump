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

        float orthoSize = (float)GetComponent<Camera>().orthographicSize;

        // offset of camera to floor depends on size of camera, + 5 for player falling off screen
        yOffset = orthoSize + 5;

        // set height for which floor should catch up to player (thus moving the camera)
        floor.GetComponent<Floor>().SetCatchupHeight(orthoSize);
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
