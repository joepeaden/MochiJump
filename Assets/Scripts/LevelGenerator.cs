using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
	private GameObject environment, mainCamera;

    [SerializeField]
    private GameObject level1Platform, level2Platform, level3Platform, level4Platform, pinballLane;

    // would be const but need to set from editor
    public float LEVEL_2_COUNT;
    public float LEVEL_3_COUNT;
    public float LEVEL_4_COUNT;

    // used for level
    private int totalEOTouched;

    private float lastEOPos;

    // used for figuring out spacing of spawning next EO
    private EnvironmentObject lastEOSpawned;

    // for debugging, don't generate level except for initial environmentObject
    [SerializeField]
    private bool generateLevel;
    // used for debugging
    private int totalEOSpawned;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        environment = new GameObject("Environment");
    
        GenerateEO(true);
    }
    
    // EO for EnvironmentObject, parent of platforms and bumpers etc.
    public void GenerateEO(bool gameStart)
    {
        // need size to spawn for length of camera
        float sizeX = mainCamera.GetComponent<Camera>().orthographicSize;

        // spawn with plenty of room ahead of player
        float sizeY = sizeX + 10;

        GameObject environmentObject;
        environmentObject = GetEnvironmentObject();

        // vertical spacing of environmentObjects
        int spacing = GetEnvironmentObjectSpacing();

        float range = sizeX / 2;

        // float[] posx for the # of environmentObjects decided at this height lvl
        // positions for spawning environmentObject
        float posx = Random.Range(-range, range);
        float posy = lastEOPos + spacing;

        GameObject currentEO = null;
        if (gameStart)
        {
            // spawn initial environmentObject
            currentEO = Instantiate(level1Platform, new Vector3(0f, -1.5f, 0f), Quaternion.identity, environment.transform);
            currentEO.GetComponentInChildren<EnvironmentObject>().UpdateEOTouched = UpdateEOTouched;

            totalEOSpawned++;

            if(generateLevel)
            {
                // if at game start, then spawn collection of environmentObjects
                for (int i = 0; i < sizeY; i++)
                {
                    posx = Random.Range(-range, range);
                    posy = 0 + i;

                    // every <spacing value> units, spawn a environmentObject with a random x position
                    if (i % spacing == 0 && i != 0)
                    {
                        // put all of the environmentObjects under the environment parent object
                        currentEO = Instantiate(environmentObject, new Vector3(posx, posy, 0), Quaternion.identity, environment.transform);

                        currentEO.GetComponentInChildren<EnvironmentObject>().UpdateEOTouched = UpdateEOTouched;

                        totalEOSpawned++;
                    }

                    // save last position to make new environmentObject in correct place
                    if (currentEO != null)
                        lastEOPos = currentEO.transform.position.y;
                }
            }
        }
        // if not at game start, just spawn a single environmentObject, then break 
        else if (generateLevel)
        {
            currentEO = Instantiate(environmentObject, new Vector3(posx, posy, 0), Quaternion.identity, environment.transform);

            EnvironmentObject eo_Script = currentEO.GetComponentInChildren<EnvironmentObject>();
            eo_Script.UpdateEOTouched = UpdateEOTouched;

            // lanes need to orient themselves based on prev object position
            PinballLane lane = currentEO.GetComponent<PinballLane>();
            if (lane)
            {
                lane.prevEO = lastEOSpawned.gameObject;
            }

            // save last position to make new environmentObject in correct place
            lastEOPos = currentEO.transform.position.y;

            totalEOSpawned++;
        }

        lastEOSpawned = currentEO?.GetComponentInChildren<EnvironmentObject>();
    }

    private int GetEnvironmentObjectSpacing()
    {
        if (lastEOSpawned?.type == EnvironmentObject.EOType.Bumper)
            return 30;
        
        return 10;
    }

    // Will be updated by environmentObjects when they are touched by player.
    public void UpdateEOTouched()
    {
        totalEOTouched++;
    }

    public void ClearEnvironment()
    {
        Transform tf = environment.transform;

        // clear all of the environmentObjects spawned
        for (int i = 0; i < tf.childCount; i++)
        {
            Destroy(tf.GetChild(i).gameObject);
        }

        totalEOTouched = 0;
        totalEOSpawned = 0;
    }

    public GameObject GetEnvironmentObject()
    {
        float value = Random.Range(0f, 100f);

        if (totalEOTouched > LEVEL_4_COUNT)
        {
            if(value > 75f)
            {
                return pinballLane;
            }
            else if (value > 25f)
            {
                return level3Platform;
            }
            else if (value > 10f)
            {
                return level2Platform;
            }
            return level1Platform;
        }
        if (totalEOTouched > LEVEL_3_COUNT)
        {
            if (value > 50f)
            {
                return level3Platform;
            }
            else if (value > 25f)
            {
                return level2Platform;
            }
            return level1Platform;
        }
        else
        if (totalEOTouched > LEVEL_2_COUNT)
        {
            if (value > 50f)
            {
                return level2Platform;
            }
            return level1Platform;
        }

        return level1Platform;
    }
}
