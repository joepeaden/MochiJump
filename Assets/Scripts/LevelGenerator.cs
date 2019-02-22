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

        // float[] posx for the # of platforms decided at this height lvl
        // positions for spawning platform
        float posx = Random.Range(-range, range);
        float posy = lastPlatPos + spacing;

        GameObject platform;

        // deciding type of platform
        // don't offer fallingPlatform as an option until certain point
        if (posy >= 100f && Random.Range(0f, 3f) <= 1)
            platform = fallingPlatform;
        else
            platform = basicPlatform;

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

}

//class Level
//{
//    // min max spacing
//    int minSpacing;
//    int maxSpacing;

//    // min and max platforms per height level
//    int minPlatsX;
//    int maxPlatsX;

//    GameObject[] possiblePlatforms;

//    public Level(int minSpacing, int maxSpacing, int minSpacing, int minPlatsX, GameObject[] possiblePlatforms)
//    {
//        this.minSpacing = minSpacing;
//        this.maxSpacing = maxSpacing;
//        this.minPlatsX = minPlatsX;
//        this.maxPlatsX = maxPlatsX;
//        this.possiblePlatforms = possiblePlatforms;
//    }

//}
