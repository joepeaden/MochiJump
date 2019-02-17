using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
	private static GameObject environment, basicPlatform, mainCamera, fallingPlatform;

    private static Vector2 dimensions;

	private static float lastPlatPos;

	void Start()
    {
		basicPlatform = Resources.Load<GameObject>("Prefabs/Platforms/BasicPlatform");
        fallingPlatform = Resources.Load<GameObject>("Prefabs/Platforms/FallingPlatform");

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

		// need dimensions to generate new chunks, & dimensions do not change
		dimensions = mainCamera.GetComponent<BoxCollider2D>().size;

		environment = new GameObject("Environment");
    
        GeneratePlatform(true);
    }
    
    public static void GeneratePlatform(bool gameStart)
    {
        // need size to spawn for length of camera
        float sizeX = mainCamera.GetComponent<Camera>().orthographicSize;

        // spawn with plenty of room ahead of player
        float sizeY = sizeX + 10;

        // vertical spacing of platforms
        int spacing = 10;

        float range = sizeX / 2;

        // positions for spawning platform
        float posx = Random.Range(-range, range);
        float posy = lastPlatPos + spacing;

        GameObject platform;

        // deciding type of platform
        if (Random.Range(0f, 3f) > 1)
            platform = basicPlatform;
        else
            platform = fallingPlatform;

        // if not at game start, just spawn a single platform, then break 
        if (!gameStart)
        {
            GameObject currentPlatform = Instantiate(platform, new Vector3(posx, posy, 0), Quaternion.identity, environment.transform);

            // save last position to make new platform in correct place
            lastPlatPos = currentPlatform.transform.position.y;

            return;
        }

        // if at game start, then spawn collection of platforms
        for (int i = 0; i < sizeY; i++)
        {
            posx = Random.Range(-range, range);
            posy = 0 + i;
            GameObject currentPlatform = null;

            // every <spacing value> units, spawn a platform with a random x position
            if (i % spacing == 0 && i != 0)
            {            
                // put all of the platforms under the environment parent object
                currentPlatform = Instantiate(platform, new Vector3(posx, posy, 0), Quaternion.identity, environment.transform);
            }

            // save last position to make new platform in correct place
            if(currentPlatform != null)
                lastPlatPos = currentPlatform.transform.position.y;
        }
    }

    public static void ClearEnvironment()
    {
        Transform tf = environment.transform;

        // clear all of the platforms spawned
        for (int i = 0; i < tf.childCount; i++)
        {
            Destroy(tf.GetChild(i).gameObject);
        }
    }

    //private void DeleteOldChunk()
    //{
    //	for(int i = 0; i < existingChunks[0].Length; i++)
    //		Destroy(existingChunks[0][i]);

    //	existingChunks.RemoveAt(0);
    //}

    //public static void GenerateChunk(bool firstChunk)
    //{
    //	float baseHeight;
    //	if(firstChunk)
    //		 if first chunk, spawn starting at camera position
    //		baseHeight = mainCamera.GetComponent<Transform>().position.y;
    //	else
    //		baseHeight = mainCamera.GetComponent<Transform>().position.y + dimensions.y/2;

    //	 vertical spacing of platforms
    //	int spacing = 6;

    //	if(existingChunks == null)
    //		existingChunks = new List<GameObject[]>();

    //	int platformIndex = 0;
    //	int numBlocks = (int) dimensions.y / spacing; 
    //	GameObject[] chunk = new GameObject[numBlocks];

    //	for(int i = 0; i < dimensions.y; i++)
    //	{
    //		 every <spacing value> units, spawn a platform with a random x position
    //		if(i % spacing == 0 && i != 0)
    //		{
    //			float posy = baseHeight + i;
    //			float range = dimensions.x/2;
    //			float posx = Random.Range(-range, range);

    //			 put all of the platforms under the environment parent object
    //			GameObject currentPlatform = Instantiate(platform, new Vector3(posx,posy,0), Quaternion.identity, environment.transform);					

    //			chunk[platformIndex] = currentPlatform;
    //			platformIndex++;
    //		}
    //	}

    //	 add chunk to list of chunks
    //	existingChunks.Add(chunk);
    //}
}
