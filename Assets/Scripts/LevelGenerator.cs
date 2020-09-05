using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class LevelGenerator : MonoBehaviour
{
	private GameObject environment, mainCamera;
    
    public int level;

    // limit for each level, once passed next level will be triggered
    public int[] levelPlatformTouchedLimits;
    
    // platforms, etc. to spawn
    public GameObject[] environmentObjects;
    
    // Used to cache environment objects that are active in current level
    public GameObject[] levelEnvironmentObjects;
    
    // used for level
    private int totalEOTouched;

    private Vector2 lastObjectPosition;

    // used for figuring out spacing of spawning next EO
    private EnvironmentObject lastEOSpawned;

    // for debugging, don't generate level except for initial environmentObject
    [SerializeField]
    private bool generateLevel;
    // used for debugging
    private int totalEOSpawned;

    [SerializeField]
    private UIManager ui;

    [SerializeField]
    private Player player;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        environment = new GameObject("Environment");

        //SetupPlats();

        InitLevel(true);

        GenerateEO(true);
    }

    // EO for EnvironmentObject, parent of platforms and bumpers etc.
    public void GenerateEO(bool gameStart)
    {
        // if there is a next level and we're at the point to transition to next level
        if ((levelPlatformTouchedLimits.Length-1) > level && totalEOTouched > levelPlatformTouchedLimits[level])
            InitLevel(false);

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

        // want pinball lanes to be able to be directly above player
        // I have a feeling I'm gonna remove pinball lanes...
        if (environmentObject.GetComponentInChildren<EnvironmentObject>().type != EnvironmentObject.EOType.Bumper)
        {
            // don't let platforms spawn directly on top of other platforms
            while (Mathf.Abs(posx - lastObjectPosition.x) < 4f)
            {
                posx = Random.Range(-range, range);
            }
        }

        float posy = lastObjectPosition.y + spacing;

        GameObject currentEO = null;
        if (gameStart)
        {
            // spawn initial environmentObject
            currentEO = Instantiate(environmentObjects[0], new Vector3(0f, -1.5f, 0f), Quaternion.identity, environment.transform);
            EnvironmentObject eo_Script = currentEO.GetComponentInChildren<EnvironmentObject>();
            eo_Script.UpdateEOTouched = UpdateEOTouched;
            eo_Script.ui = ui;

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

                        eo_Script = currentEO.GetComponentInChildren<EnvironmentObject>();
                        eo_Script.UpdateEOTouched = UpdateEOTouched;
                        eo_Script.ui = ui;

                        totalEOSpawned++;
                    }

                    // save last position to make new environmentObject in correct place
                    if (currentEO != null)
                        lastObjectPosition = currentEO.transform.position;
                }
            }
        }
        // if not at game start, just spawn a single environmentObject, then break 
        else if (generateLevel)
        {
            currentEO = Instantiate(environmentObject, new Vector3(posx, posy, 0), Quaternion.identity, environment.transform);

            EnvironmentObject eo_Script = currentEO.GetComponentInChildren<EnvironmentObject>();
            eo_Script.UpdateEOTouched = UpdateEOTouched;
            eo_Script.ui = ui;

            // lanes need to orient themselves based on prev object position
            PinballLane lane = currentEO.GetComponent<PinballLane>();
            if (lane)
            {
                lane.prevEO = lastEOSpawned.gameObject;
            }

            // save last position to make new environmentObject in correct place
            lastObjectPosition = currentEO.transform.position;

            totalEOSpawned++;
        }

        lastEOSpawned = currentEO?.GetComponentInChildren<EnvironmentObject>();
    }

    private void InitLevel(bool gameStart)
    {
        // don't want to increase level when at game start
        if (!gameStart)
        {
            level++;
        }

        List<GameObject> tempList = new List<GameObject>();

        // loop through each environment object and add it to list if it's active
        foreach (GameObject obj in environmentObjects)
        {
            if(obj.GetComponentInChildren<EnvironmentObject>().ActiveInLevel(level))
            {
                tempList.Add(obj);
            }
        }

        levelEnvironmentObjects = tempList.ToArray();

    }

    private int GetEnvironmentObjectSpacing()
    {
        if (lastEOSpawned?.type == EnvironmentObject.EOType.Bumper)
            return 30;
        
        return 10;
    }

    // Will be updated by environmentObjects when they are touched by player.
    public void UpdateEOTouched(int pointValue)
    {
        totalEOTouched++;
        player.AddPoints(pointValue);
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
        level = 0;
        InitLevel(true);
    }

    public GameObject GetEnvironmentObject()
    {
        float total = 0;

        foreach (GameObject obj in levelEnvironmentObjects)
        {
            total += obj.GetComponentInChildren<EnvironmentObject>().SpawnChances[level];
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i < levelEnvironmentObjects.Length; i++)
        {
            EnvironmentObject eoScript = levelEnvironmentObjects[i].GetComponentInChildren<EnvironmentObject>();
            if (randomPoint < eoScript.SpawnChances[level])
            {
                return levelEnvironmentObjects[i];
            }
            else
            {
                randomPoint -= eoScript.SpawnChances[level];
            }
        }
        return levelEnvironmentObjects[levelEnvironmentObjects.Length - 1];
    }

}
