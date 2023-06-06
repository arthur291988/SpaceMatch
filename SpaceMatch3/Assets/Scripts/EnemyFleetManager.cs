using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFleetManager : MonoBehaviour
{
    [NonSerialized]
    public List<EnemyShip> enemyFleet;
    [NonSerialized]
    public EnemyShip nextShipToEnergy;
    [NonSerialized]
    public EnemyShip nextShipToShot;
    [NonSerialized]
    public EnemyShip nextShipToHP;
    [NonSerialized]
    public EnemyShip nextShipToShield;

    // Start is called before the first frame update

    public static EnemyFleetManager instance;
    private float HPAddValue;
    private float ShieldAddValue;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {

        enemyFleet = new List<EnemyShip>();
    }

    //void Start()
    //{
    //}

    public void startSettings() {
        nextShipToEnergy = enemyFleet[0];
        nextShipToShot = enemyFleet[0];
        nextShipToHP = enemyFleet[0];
        nextShipToShield = enemyFleet[0];
        HPAddValue = 0.2f;
        ShieldAddValue = 0.3f; //uses energy
    }

    public void assignNextShipToEnergy () {
        if (enemyFleet.Count > 1)
        {
            int x = enemyFleet.IndexOf(nextShipToEnergy) + 1;
            if (x < enemyFleet.Count) nextShipToEnergy = enemyFleet[x];
            else nextShipToEnergy = enemyFleet[0];
        }
        else if (enemyFleet.Count>0) nextShipToEnergy = enemyFleet[0];
    }

    public void assignNextShipToEnergyIfThisDestroyed(EnemyShip ship) {
        if (ship == nextShipToEnergy) {
            if (enemyFleet.Count > 1)
            {
                int x = enemyFleet.IndexOf(ship);
                if (x < enemyFleet.Count - 1) nextShipToEnergy = enemyFleet[x + 1];
                else nextShipToEnergy = enemyFleet[0];
            }
        }
    }

    public void assignNextShipToHP()
    {
        if (enemyFleet.Count > 1)
        {
            int x = enemyFleet.IndexOf(nextShipToHP) + 1;
            if (x < enemyFleet.Count) nextShipToHP = enemyFleet[x];
            else nextShipToHP = enemyFleet[0];
        }
        else if (enemyFleet.Count > 0) nextShipToHP = enemyFleet[0];
    }

    public void assignNextShipToHPIfThisDestroyed(EnemyShip ship)
    {
        if (ship == nextShipToHP)
        {
            if (enemyFleet.Count > 1)
            {
                int x = enemyFleet.IndexOf(ship);
                if (x < enemyFleet.Count - 1) nextShipToHP = enemyFleet[x + 1];
                else nextShipToHP = enemyFleet[0];
            }
        }
    }

    public void assignNextShipToShot()
    {
        if (enemyFleet.Count > 1)
        {
            int x = enemyFleet.IndexOf(nextShipToShot) + 1;
            if (x < enemyFleet.Count) nextShipToShot = enemyFleet[x];
            else nextShipToShot = enemyFleet[0];
        }
        else if (enemyFleet.Count > 0) nextShipToShot = enemyFleet[0];
    }

    public void assignNextShipToShotIfThisDestroyed(EnemyShip ship)
    {
        if (ship == nextShipToShot)
        {
            if (enemyFleet.Count > 1)
            {
                int x = enemyFleet.IndexOf(ship);
                if (x < enemyFleet.Count - 1) nextShipToShot = enemyFleet[x + 1];
                else nextShipToShot = enemyFleet[0];
            }
        }
    }

    public void assignNextShipToShield()
    {
        if (enemyFleet.Count > 1)
        {
            int x = enemyFleet.IndexOf(nextShipToShot) + 1;
            if (x < enemyFleet.Count) nextShipToShield = enemyFleet[x];
            else nextShipToShield = enemyFleet[0];
        }
        else if (enemyFleet.Count > 0) nextShipToShield = enemyFleet[0];
    }

    public void assignNextShipToShieldIfThisDestroyed(EnemyShip ship)
    {
        if (ship == nextShipToShield)
        {
            if (enemyFleet.Count > 1)
            {
                int x = enemyFleet.IndexOf(ship);
                if (x < enemyFleet.Count - 1) nextShipToShield = enemyFleet[x + 1];
                else nextShipToShield = enemyFleet[0];
            }
        }
    }

    public void distributeResources(int index, int value)
    {
        if (index == 0)
        {
            for (int i = 0; i < value; i++)
            {
                nextShipToShot.shotEnergy++;
                assignNextShipToShot();
            }
        }
        else if (index == 1)
        {
            for (int i = 0; i < value; i++)
            {
                nextShipToEnergy.energy++;
                assignNextShipToEnergy();
            }
        }
        else if (index == 2)
        {
            for (int i = 0; i < value; i++)
            {
                if (nextShipToShield.shield.activeInHierarchy) nextShipToShield.healShield(ShieldAddValue);
                else nextShipToShield.cumulateShiled(ShieldAddValue);

                assignNextShipToShield();
            }
        }
        else if (index == 3)
        {
            for (int i = 0; i < value; i++)
            {
                nextShipToHP.healHP(HPAddValue);
                assignNextShipToHP();
            }
        }
        
    }

    public void checkActionsOfFleet()
    {
        foreach (EnemyShip ship in enemyFleet) ship.checkActions();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
