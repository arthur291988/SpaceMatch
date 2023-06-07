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


    private void Awake()
    {
        instance = this;
        xStep = 2.2f;
    }

    // Start is called before the first frame update
    void Start()
    {
        instantiateEnemyFleet();
        instantiatePlayerFleet();
        PlayerFleetManager.instance.startSettings();
        EnemyFleetManager.instance.startSettings();
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

            ObjectPulledList = ObjectPuller.current.GetPlayerShipPullList();
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
