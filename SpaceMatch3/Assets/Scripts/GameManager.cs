using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    //[NonSerialized]
    //public float turnTimeUp;
    //[NonSerialized]
    //public float turnTimeDown;
    //[NonSerialized]
    //public float turnTimeMax;

    //private bool timerIsOn;

    //public GameObject timerObj;
    //public Image UpTimerImg;
    //public Image DownTimerImg;

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
        //turnTimeMax = 7;

        //turnTimeUp = 7;
        //turnTimeDown = 0;
        //setTheTimer();
    }

    //private void setTheTimer() {
    //    turnTimeUp = turnTimeMax;
    //    UpTimerImg.fillAmount = turnTimeUp;
    //    turnTimeDown = 0;
    //    DownTimerImg.fillAmount = turnTimeDown;
    //    if (!timerObj.activeInHierarchy) timerObj.SetActive(true);
    //    timerIsOn = true;
    //}

    //private void updateTimerUI ()
    //{
    //    UpTimerImg.fillAmount = turnTimeUp/ turnTimeMax;
    //    DownTimerImg.fillAmount = turnTimeDown / turnTimeMax;
    //}

    //public void stopTheTimer()
    //{
    //    timerIsOn = false;
    //    timerObj.SetActive(false);
    //    Invoke("startTimerIfNoAttack", 1);
    //}

    //private void startTimerIfNoAttack()
    //{
    //    if (!fightIsOn && !GridManager.Instance.tilesAreMoving) setTheTimer();
    //}

    private void instantiateEnemyFleet() {
        float xStepLocal=0;
        for (int i =0;i<5;i++) {

            ObjectPulledList = ObjectPuller.current.GetEnemyShipPullList(2);
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

            ObjectPulledList = ObjectPuller.current.GetPlayerShipPullList(2);
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
        //if (!fightIsOn) setTheTimer();
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
    //void Update()
    //{
    //    if (!fightIsOn && timerIsOn) {
    //        turnTimeUp-=0.02f;
    //        turnTimeDown += 0.02f;
    //        updateTimerUI();
    //        if (turnTimeUp <= 0) {
    //            stopTheTimer();
    //            GridManager.Instance.CPUAttackProcess();
    //        } 
    //    }
    //}
}
