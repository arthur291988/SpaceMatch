using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonData : MonoBehaviour
{
    public static CommonData Instance { get; private set; }
    public Camera _camera;
    [NonSerialized]
    public float vertScreenSize;
    [NonSerialized]
    public float horisScreenSize;

    // key is level and value is list of counts of ship types
    //0 index- destroyers count (destroyers must be even number always, because they put two on one ship place), 1 index - cruisers count, 2 index - falgships count
    private Dictionary<int, List<int>> playerFleetByLevel;

    // key is level and value is list of counts of ship types
    //0 index- destroyers count (destroyers must be even number always, because they put two on one ship place), 1 index - cruisers count, 2 index - falgships count, 3 - admiral ship
    private Dictionary<int, List<int>> enemyFleetByLevel;

    private List<int> heardnessListByLevel;

    private int gameLevel;

    private int levelHardness; //0-easey 1-medium 2-hard

    void Awake()
    {
        Instance = this;

        //determine the sizes of view screen
        vertScreenSize = _camera.orthographicSize * 2;
        horisScreenSize = vertScreenSize * Screen.width / Screen.height;

        playerFleetByLevel = new Dictionary<int, List<int>>
        {
            [0] = new List<int> { 3, 3, 3 },
            [1] = new List<int> { 5, 5, 5 },
            [2] = new List<int> { 5, 5, 5 },
            [3] = new List<int> { 5, 5, 5 },
            [4] = new List<int> { 5, 5, 5 },
            [5] = new List<int> { 5, 5, 5 },
            [6] = new List<int> { 5, 5, 5 }
        };
        enemyFleetByLevel = new Dictionary<int, List<int>>
        {
            [0] = new List<int> { 3, 3, 3, 0 },
            [1] = new List<int> { 5, 5, 5, 0 },
            [2] = new List<int> { 5, 5, 5, 0 },
            [3] = new List<int> { 5, 5, 5, 0 },
            [4] = new List<int> { 5, 5, 5, 0 },
            [5] = new List<int> { 5, 5, 5, 0 },
            [6] = new List<int> { 5, 5, 5, 1 }
        };

        heardnessListByLevel = new List<int> { 1, 0, 1, 1, 1, 2, 2 }; //index is key and value is heardness level 
    }

    public void setGameLevelAndHardness(int value) {
        gameLevel = value;
        levelHardness = heardnessListByLevel[gameLevel];
    }

    public int getGameLevel() { return gameLevel; }
    public int getGameHardness() { return levelHardness; }

    public List<int> getPlayerFleetByLevel(int level) {
        return playerFleetByLevel[level];
    }
    public List<int> getEnemyFleetByLevel(int level)
    {
        return enemyFleetByLevel[level];
    }

}
