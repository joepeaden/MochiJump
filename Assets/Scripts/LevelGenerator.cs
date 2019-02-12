using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
	private static GameObject platform, environment, mainCamera;

	private static List<GameObject[]> existingChunks;

	private static Vector2 dimensions;

	private static float newChunkTrigger, deltaYPosition;

	void Start()
    {
		platform = Resources.Load<GameObject>("Prefabs/Platforms/BasicPlatform");

		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

		// need dimensions to generate new chunks, & dimensions do not change
		dimensions = mainCamera.GetComponent<BoxCollider2D>().size;

		// initial trigger is at 1/2 height of the dimensions of the boundary
		// newChunkTrigger = dimensions.y/2;

		environment = new GameObject("Environment");
    
        GeneratePlatform(true);
        
        //GenerateChunk(true);
    }

	void Update()
	{
	//	// cameraPos changes each frame
	//	Vector3 cameraPos = mainCamera.GetComponent<Transform>().position;

	//	if(cameraPos.y >= newChunkTrigger)
	//	{
	//		// get change in camera y position, to set new chunk trigger
	//		deltaYPosition = cameraPos.y - deltaYPosition;

	//		GenerateChunk(false);

	//		newChunkTrigger += deltaYPosition;

	//		// don't need to hold on to chunks no longer being used
	//		if(existingChunks.Count >= 4)
	//			DeleteOldChunk();
		
	//		// restart movement counting
	//		deltaYPosition = cameraPos.y;
	//	}
	}

    public void GeneratePlatform(bool gameStart)
    {
        // need size to spawn for length of camera
        float sizeX = mainCamera.GetComponent<Camera>().orthographicSize;

        // spawn with plenty of room ahead of player
        float sizeY = sizeX + 10;

        // vertical spacing of platforms
        int spacing = 6;

        for (int i = 0; i < sizeY; i++)
        {
            // every <spacing value> units, spawn a platform with a random x position
            if (i % spacing == 0 && i != 0)
            {
                float posy = 0 + i;
                float range = sizeX / 2;
                float posx = Random.Range(-range, range);

                // put all of the platforms under the environment parent object
                GameObject currentPlatform = Instantiate(platform, new Vector3(posx, posy, 0), Quaternion.identity, environment.transform);

                //chunk[platformIndex] = currentPlatform;
                //platformIndex++;
            }

            if (!gameStart)
                // break out of loop, to only generate single platform
                break;
        }
    }

	private void DeleteOldChunk()
	{
		for(int i = 0; i < existingChunks[0].Length; i++)
			Destroy(existingChunks[0][i]);

		existingChunks.RemoveAt(0);
	}
		
	public static void GenerateChunk(bool firstChunk)
	{
		float baseHeight;
		if(firstChunk)
			// if first chunk, spawn starting at camera position
			baseHeight = mainCamera.GetComponent<Transform>().position.y;
		else
			baseHeight = mainCamera.GetComponent<Transform>().position.y + dimensions.y/2;
		
		// vertical spacing of platforms
		int spacing = 6;

		if(existingChunks == null)
			existingChunks = new List<GameObject[]>();

		int platformIndex = 0;
		int numBlocks = (int) dimensions.y / spacing; 
		GameObject[] chunk = new GameObject[numBlocks];

		for(int i = 0; i < dimensions.y; i++)
		{
			// every <spacing value> units, spawn a platform with a random x position
			if(i % spacing == 0 && i != 0)
			{
				float posy = baseHeight + i;
				float range = dimensions.x/2;
				float posx = Random.Range(-range, range);

				// put all of the platforms under the environment parent object
				GameObject currentPlatform = Instantiate(platform, new Vector3(posx,posy,0), Quaternion.identity, environment.transform);					

				chunk[platformIndex] = currentPlatform;
				platformIndex++;
			}
		}

		// add chunk to list of chunks
		existingChunks.Add(chunk);
	}

	public static void ClearEnvironment()
	{
		Transform tf = environment.transform;

		// clear all of the platforms spawned
		for(int i = 0; i < tf.childCount; i++)
		{
			Destroy(tf.GetChild(i).gameObject);
		}
	}
}
