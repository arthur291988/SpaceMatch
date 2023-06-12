using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPuller : MonoBehaviour
{
    public static ObjectPuller current;


    private int pullOfObjects100 = 100;
    private int pullOfObjects20 = 20;
    private int pullOfObjects10 = 10;
    private bool willGrow;


    [SerializeField]
    private GameObject Tiles;
    [SerializeField]
    private GameObject playerFlagshipShot;
    [SerializeField]
    private GameObject enemyShot;

    [SerializeField]
    private GameObject playerFlagship;
    [SerializeField]
    private GameObject enemyShip;

    private List<GameObject> TilesList;
    private List<GameObject> enemyShotList;
    private List<GameObject> playerFlagshipShotList;

    private List<GameObject> enemyShipList;
    private List<GameObject> playerFlagshipList;


    private void Awake()
    {
        willGrow = true;
        current = this;
    }

    private void OnEnable()
    {
        TilesList = new List<GameObject>();

        enemyShotList = new List<GameObject>();
        playerFlagshipShotList = new List<GameObject>();

        enemyShipList = new List<GameObject>();
        playerFlagshipList = new List<GameObject>();


        for (int i = 0; i < pullOfObjects100; i++)
        {
            GameObject obj1 = Instantiate(Tiles);
            obj1.SetActive(false);
            TilesList.Add(obj1);

        }

        for (int i = 0; i < pullOfObjects20; i++)
        {
            GameObject obj1 = Instantiate(enemyShot);
            obj1.SetActive(false);
            enemyShotList.Add(obj1);

            GameObject obj2 = Instantiate(playerFlagshipShot);
            obj2.SetActive(false);
            playerFlagshipShotList.Add(obj2);

        }

        for (int i = 0; i < pullOfObjects10; i++)
        {
            GameObject obj1 = Instantiate(enemyShip);
            obj1.SetActive(false);
            enemyShipList.Add(obj1);

            GameObject obj2 = Instantiate(playerFlagship);
            obj2.SetActive(false);
            playerFlagshipList.Add(obj2);

        }

    }

    public List<GameObject> GetTilePullList()
    {
        return TilesList;
    }
    public List<GameObject> GetPlayerFlagshipShotPullList()
    {
        return playerFlagshipShotList;
    }
    public List<GameObject> GetEnemyShotPullList()
    {
        return enemyShotList;
    }

    public List<GameObject> GetPlayerFlagshipPullList()
    {
        return playerFlagshipList;
    }
    public List<GameObject> GetEnemyShipPullList()
    {
        return enemyShipList;
    }



    //universal method to set active proper game object from the list of GOs, it just needs to get correct List of game objects
    public GameObject GetGameObjectFromPull(List<GameObject> GOLists)
    {
        for (int i = 0; i < GOLists.Count; i++)
        {
            if (!GOLists[i].activeInHierarchy) return GOLists[i];
        }
        if (willGrow)
        {
            GameObject obj = Instantiate(GOLists[0]);
            obj.SetActive(false);
            GOLists.Add(obj);
            return obj;
        }
        return null;
    }
}
