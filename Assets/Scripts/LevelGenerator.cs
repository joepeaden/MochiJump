using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
	private GameObject environment, mainCamera;

    private GameObject basicPlatform, fallingPlatform, movingPlatform, bounceUpPlatform;

    // would be const but need to set from editor
    public float LEVEL_2_COUNT;
    public float LEVEL_3_COUNT;

    // used for level
    private int totalPlatformsTouched;

    private float lastPlatPos;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        basicPlatform = Resources.Load<GameObject>("Prefabs/Platforms/BasicPlatform");
        fallingPlatform = Resources.Load<GameObject>("Prefabs/Platforms/FallingPlatform");
        movingPlatform = Resources.Load<GameObject>("Prefabs/Platforms/MovingPlatform");
        bounceUpPlatform = Resources.Load<GameObject>("Prefabs/Platforms/BounceUp");

        environment = new GameObject("Environment");
    
        GeneratePlatform(true);
    }
    
    public void GeneratePlatform(bool gameStart)
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

        platform = GetPlatform();

        // if not at game start, just spawn a single platform, then break 
        if (!gameStart)
        {
            GameObject currentPlatform = Instantiate(platform, new Vector3(posx, posy, 0), Quaternion.identity, environment.transform);

            currentPlatform.GetComponentInChildren<Platform>().UpdatePlatformsTouched = UpdatePlatformsTouched;

            // save last position to make new platform in correct place
            lastPlatPos = currentPlatform.transform.position.y;
        }
        else
        {
            // spawn initial platform
            GameObject currentPlatform = Instantiate(basicPlatform, new Vector3(0f, -1.5f, 0f), Quaternion.identity, environment.transform);
            currentPlatform.GetComponentInChildren<Platform>().UpdatePlatformsTouched = UpdatePlatformsTouched;

            // if at game start, then spawn collection of platforms
            for (int i = 0; i < sizeY; i++)
            {
                posx = Random.Range(-range, range);
                posy = 0 + i;

                // every <spacing value> units, spawn a platform with a random x position
                if (i % spacing == 0 && i != 0)
                {
                    // put all of the platforms under the environment parent object
                    currentPlatform = Instantiate(platform, new Vector3(posx, posy, 0), Quaternion.identity, environment.transform);

                    currentPlatform.GetComponentInChildren<Platform>().UpdatePlatformsTouched = UpdatePlatformsTouched;
                }

                // save last position to make new platform in correct place
                if (currentPlatform != null)
                    lastPlatPos = currentPlatform.transform.position.y;
            }
        }
    }

    // Will be updated by platforms when they are touched by player.
    public void UpdatePlatformsTouched()
    {
        totalPlatformsTouched++;
    }

    public void ClearEnvironment()
    {
        Transform tf = environment.transform;

        // clear all of the platforms spawned
        for (int i = 0; i < tf.childCount; i++)
        {
            Destroy(tf.GetChild(i).gameObject);
        }

        totalPlatformsTouched = 0;
    }

    public GameObject GetPlatform()
    {
        float value = Random.Range(0f, 100f);

        if (totalPlatformsTouched > LEVEL_3_COUNT)
        {
            // for now to avoid bug with moving platforms
            return fallingPlatform;

            //if (value > 25f)
            //{
            //    return movingPlatform;
            //}
            //else if (value > 10f)
            //{
            //    return fallingPlatform;
            //}
            //return basicPlatform;
        }
        else
        if (totalPlatformsTouched > LEVEL_2_COUNT)
        {
            if (value > 50f)
            {
                return fallingPlatform;
            }
            return basicPlatform;
        }

        return basicPlatform;
    }
}
