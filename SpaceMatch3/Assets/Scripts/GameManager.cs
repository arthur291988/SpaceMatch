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


    private bool fightIsOn;
    private bool movesFrozen;
    [SerializeField]
    private GameObject coverBoard;

    [NonSerialized]
    public List<Shot> shotsOnScene;

    private const float firstRowYValueEnemyFleet = 11;
    private const float secondRowYValueEnemyFleet = 8.45f;
    private const float firstRowYValuePlayerFleet = 0;
    private const float secondRowYValuePlayerFleet = 2.55f;
    private const float destroyerXGap = 0.5f;
    //private const float destroyerYGap = 0.35f;

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
        CommonData.Instance.setGameLevelAndHardness(0);
        instantiateEnemyFleet();
        instantiatePlayerFleet();
        PlayerFleetManager.instance.startSettings();
        EnemyFleetManager.instance.startSettings();
        fightIsOn = false;
        movesFrozen = false;

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



    //iterate through types of ships on first cycle and their counts on second level of cycle
    //forst level of cycle iterates downwards because stronger ships put on scene first 
    private void instantiateEnemyFleet()
    {
        int totalShipsCount = 0;
        float xStepLocal = 0;
        float yStepLocal = firstRowYValueEnemyFleet;
        GameObject shipObject;
        GameObject shipObject2;
        for (int i = CommonData.Instance.getEnemyFleetByLevel(CommonData.Instance.getGameLevel()).Count - 1; i >= 0; i--)
        {
            for (int j = 0; j < CommonData.Instance.getEnemyFleetByLevel(CommonData.Instance.getGameLevel())[i]; j++)
            {
                shipObject = pullEnemyShip(i);

                if (totalShipsCount == 0) shipObject.transform.position = new Vector2(xStepLocal, yStepLocal);
                //switch to second row of fleet
                else if (totalShipsCount == 5)
                {
                    yStepLocal = secondRowYValueEnemyFleet;
                    xStepLocal = 0;
                    shipObject.transform.position = new Vector2(xStepLocal, yStepLocal);
                }
                if (yStepLocal != secondRowYValueEnemyFleet)
                {
                    if (totalShipsCount % 2 == 0)
                    {
                        shipObject.transform.position = new Vector2(-xStepLocal, yStepLocal);
                    }
                    else
                    {
                        xStepLocal += xStep;
                        shipObject.transform.position = new Vector2(xStepLocal, yStepLocal);
                    }
                }
                else
                {
                    if (totalShipsCount % 2 == 0)
                    {
                        xStepLocal += xStep;
                        shipObject.transform.position = new Vector2(xStepLocal, yStepLocal);
                    }
                    else
                    {
                        shipObject.transform.position = new Vector2(-xStepLocal, yStepLocal);
                    }
                }
                Ship ship = shipObject.GetComponent<Ship>();
                ship.StartSettings();
                shipObject.SetActive(true);
                ship.activatePowerShiledOnStart();

                //if ship is detroyer there necessery to put extra one in one placement
                if (i == 0)
                {
                    shipObject2 = pullEnemyShip(i);
                    float basePointX = shipObject.transform.position.x;
                    shipObject.transform.position = new Vector2(basePointX + destroyerXGap, yStepLocal);
                    shipObject2.transform.position = new Vector2(basePointX - destroyerXGap, yStepLocal);

                    Ship ship2 = shipObject2.GetComponent<Ship>();
                    ship2.StartSettings();
                    shipObject2.SetActive(true);
                    ship2.activatePowerShiledOnStart();
                }


                

                totalShipsCount++;
            }
        }
    }
    private void instantiatePlayerFleet()
    {
        int totalShipsCount = 0;
        float xStepLocal = 0;
        float yStepLocal = firstRowYValuePlayerFleet;
        GameObject shipObject;
        GameObject shipObject2;
        for (int i = CommonData.Instance.getPlayerFleetByLevel(CommonData.Instance.getGameLevel()).Count - 1; i >= 0; i--)
        {
            for (int j = 0; j < CommonData.Instance.getPlayerFleetByLevel(CommonData.Instance.getGameLevel())[i]; j++)
            {

                shipObject = pullPlayerShip(i);

                if (totalShipsCount == 0) shipObject.transform.position = new Vector2(xStepLocal, yStepLocal);
                //switch to second row of fleet
                else if (totalShipsCount ==5)
                {
                    yStepLocal = secondRowYValuePlayerFleet;
                    xStepLocal = 0;
                    shipObject.transform.position = new Vector2(xStepLocal, yStepLocal);
                }
                if (yStepLocal != secondRowYValuePlayerFleet)
                {
                    if (totalShipsCount % 2 == 0)
                    {
                        shipObject.transform.position = new Vector2(-xStepLocal, yStepLocal);
                    }
                    else
                    {
                        xStepLocal += xStep;
                        shipObject.transform.position = new Vector2(xStepLocal, yStepLocal);
                    }
                }
                else {
                    if (totalShipsCount % 2 == 0)
                    {
                        xStepLocal += xStep;
                        shipObject.transform.position = new Vector2(xStepLocal, yStepLocal);
                    }
                    else
                    {
                        shipObject.transform.position = new Vector2(-xStepLocal, yStepLocal);
                    }
                }

                Ship ship = shipObject.GetComponent<Ship>();
                ship.StartSettings();
                shipObject.SetActive(true);
                ship.activatePowerShiledOnStart();

                //if ship is detroyer there necessery to put extra one in one placement
                if (i == 0)
                {
                    shipObject2 = pullPlayerShip(i);
                    float basePointX = shipObject.transform.position.x;
                    shipObject.transform.position = new Vector2(basePointX + destroyerXGap, yStepLocal);
                    shipObject2.transform.position = new Vector2(basePointX - destroyerXGap, yStepLocal);

                    Ship ship2 = shipObject2.GetComponent<Ship>();
                    ship2.StartSettings();
                    shipObject2.SetActive(true);
                    ship2.activatePowerShiledOnStart();
                }

                totalShipsCount++;
            }
        }
    }

    private GameObject pullEnemyShip(int index) {
        ObjectPulledList = ObjectPuller.current.GetEnemyShipPullList(index);
        ObjectPulled = ObjectPuller.current.GetGameObjectFromPull(ObjectPulledList);
        ObjectPulled.transform.rotation = Quaternion.Euler(0, 0, 180);
        return ObjectPulled;
    }
    private GameObject pullPlayerShip(int index)
    {
        ObjectPulledList = ObjectPuller.current.GetPlayerShipPullList(index);
        ObjectPulled = ObjectPuller.current.GetGameObjectFromPull(ObjectPulledList);
        
        return ObjectPulled;
    }

    public bool getFightIsOn() {
        return fightIsOn;
    }
    public bool getMovesFrozen()
    {
        return movesFrozen;
    }

    public void setFightOn(bool state)
    {
        fightIsOn = state;
        if (!state) {
            //chek if game finished
            if (EnemyFleetManager.instance.enemyFleet.Count!=0 && PlayerFleetManager.instance.playerFleet.Count != 0) coverBoard.SetActive(state);
            else if (!coverBoard.activeInHierarchy) coverBoard.SetActive(true);
        }
        else coverBoard.SetActive(state);
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
