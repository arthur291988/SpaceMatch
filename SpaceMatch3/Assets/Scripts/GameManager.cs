using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector]
    public GameObject ObjectPulled;
    [HideInInspector]
    public List<GameObject> ObjectPulledList;

    private float xStep;

    public float hardnessLevel; //0-easey 1-medium 2-hard

    private bool fightIsOn;
    [SerializeField]
    private GameObject coverBoard;

    [NonSerialized]
    public List<Shot> shotsOnScene;

    private void Awake()
    {
        instance = this;
        xStep = 2.2f;
    }

    // Start is called before the first frame update
    void Start()
    {
        shotsOnScene = new List<Shot>();
        instantiateEnemyFleet();
        instantiatePlayerFleet();
        PlayerFleetManager.instance.startSettings();
        EnemyFleetManager.instance.startSettings();
        hardnessLevel = 0;
        fightIsOn = false;
    }

    private void instantiateEnemyFleet() {
        float xStepLocal=0;
        for (int i =0;i<5;i++) {

            ObjectPulledList = ObjectPuller.current.GetEnemyShipPullList();
            ObjectPulled = ObjectPuller.current.GetGameObjectFromPull(ObjectPulledList);

            if (i == 0) ObjectPulled.transform.position = new Vector2(0, 11);
            else if (i % 2 == 0)
            {
                ObjectPulled.transform.position = new Vector2(-xStepLocal, 11);
            }
            else {
                xStepLocal += xStep;
                ObjectPulled.transform.position = new Vector2(xStepLocal, 11);
            }
            ObjectPulled.transform.rotation = Quaternion.Euler(0, 0, 180);
            Ship ship = ObjectPulled.GetComponent<Ship>();
            ship.StartSettings();
            ObjectPulled.SetActive(true);
            ship.activatePowerShiledOnStart();
        }

    }
    private void instantiatePlayerFleet()
    {
        float xStepLocal = 0;
        for (int i = 0; i < 5; i++)
        {

            ObjectPulledList = ObjectPuller.current.GetPlayerFlagshipPullList();
            ObjectPulled = ObjectPuller.current.GetGameObjectFromPull(ObjectPulledList);

            if (i == 0) ObjectPulled.transform.position = Vector2.zero;
            else if (i % 2 == 0)
            {
                ObjectPulled.transform.position = new Vector2(-xStepLocal, 0);
            }
            else
            {
                xStepLocal += xStep;
                ObjectPulled.transform.position = new Vector2(xStepLocal, 0);
            }
            Ship ship = ObjectPulled.GetComponent<Ship>();
            ship.StartSettings();
            ObjectPulled.SetActive(true);
            ship.activatePowerShiledOnStart();
        }

    }

    public bool getFightIsOn() {
        return fightIsOn;
    }

    public void setFightOn(bool state)
    {
        fightIsOn = state;
        coverBoard.SetActive(state);
    }

    public void addShot(Shot shot)
    {
        shotsOnScene.Add(shot);
        if (!fightIsOn) setFightOn(true);
    }
    public void removeShot(Shot shot)
    {
        shotsOnScene.Remove(shot);
        if (shotsOnScene.Count < 1 && checkAllShipsIfActionIsFinished())
            setFightOn(false);
    }

    public bool checkAllShipsIfActionIsFinished()
    {
        bool isFinished = true;
        foreach (Ship ship in PlayerFleetManager.instance.playerFleet)
        {
            if (ship.actionsAreOn)
            {
                isFinished = false;
                break;
            }
        }
        if (isFinished)
        {
            foreach (Ship ship in EnemyFleetManager.instance.enemyFleet)
            {
                if (ship.actionsAreOn)
                {
                    isFinished = false;
                    break;
                }
            }
        }

        return isFinished;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
