using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFleetManager : MonoBehaviour
{
    [NonSerialized]
    public List<PlayerShip> playerFleet;


    [NonSerialized]
    public PlayerShip nextShipToEnergy;
    [NonSerialized]
    public PlayerShip nextShipToShot;
    [NonSerialized]
    public PlayerShip nextShipToHP;
    [NonSerialized]
    public PlayerShip nextShipToShield;

    

    public static PlayerFleetManager instance;

    private float HPAddValue;
    private float ShieldAddValue;

    private void Awake()
    {
        instance = this;


    }

    private void OnEnable()
    {
        playerFleet = new List<PlayerShip>();
    }

    //void Start()
    //{


    //}
    public void startSettings()
    {
        nextShipToEnergy = playerFleet[0];
        nextShipToShot = playerFleet[0];
        nextShipToHP = playerFleet[0];
        nextShipToShield = playerFleet[0];
        HPAddValue = 0.2f;
        ShieldAddValue = 0.3f; //uses energy
    }

    public void assignNextShipToEnergy()
    {
        if (playerFleet.Count > 1)
        {
            int x = playerFleet.IndexOf(nextShipToEnergy) + 1;
            if (x < playerFleet.Count) nextShipToEnergy = playerFleet[x];
            else nextShipToEnergy = playerFleet[0];
        }
        else if (playerFleet.Count > 0) nextShipToEnergy = playerFleet[0];
    }

    public void assignNextShipToEnergyIfThisDestroyed(PlayerShip ship)
    {
        if (ship == nextShipToEnergy)
        {
            if (playerFleet.Count > 1)
            {
                int x = playerFleet.IndexOf(ship);
                if (x < playerFleet.Count - 1) nextShipToEnergy = playerFleet[x + 1];
                else nextShipToEnergy = playerFleet[0];
            }
        }
    }

    public void assignNextShipToHP()
    {
        if (playerFleet.Count > 1)
        {
            int x = playerFleet.IndexOf(nextShipToHP) + 1;
            if (x < playerFleet.Count) nextShipToHP = playerFleet[x];
            else nextShipToHP = playerFleet[0];
        }
        else if (playerFleet.Count > 0) nextShipToHP = playerFleet[0];
    }

    public void assignNextShipToHPIfThisDestroyed(PlayerShip ship)
    {
        if (ship == nextShipToHP)
        {
            if (playerFleet.Count > 1)
            {
                int x = playerFleet.IndexOf(ship);
                if (x < playerFleet.Count - 1) nextShipToHP = playerFleet[x + 1];
                else nextShipToHP = playerFleet[0];
            }
        }
    }

    public void assignNextShipToShot()
    {
        if (playerFleet.Count > 1)
        {
            int x = playerFleet.IndexOf(nextShipToShot) + 1;
            if (x < playerFleet.Count) nextShipToShot = playerFleet[x];
            else nextShipToShot = playerFleet[0];
        }
        else if (playerFleet.Count > 0) nextShipToShot = playerFleet[0];
    }

    public void assignNextShipToShotIfThisDestroyed(PlayerShip ship)
    {
        if (ship == nextShipToShot)
        {
            if (playerFleet.Count > 1)
            {
                int x = playerFleet.IndexOf(ship);
                if (x < playerFleet.Count - 1) nextShipToShot = playerFleet[x + 1];
                else nextShipToShot = playerFleet[0];
            }
        }
    }
    public void assignNextShipToShield()
    {
        if (playerFleet.Count > 1)
        {
            int x = playerFleet.IndexOf(nextShipToShot) + 1;
            if (x < playerFleet.Count) nextShipToShield = playerFleet[x];
            else nextShipToShield = playerFleet[0];
        }
        else if (playerFleet.Count > 0) nextShipToShield = playerFleet[0];
    }

    public void assignNextShipToShieldIfThisDestroyed(PlayerShip ship)
    {
        if (ship == nextShipToShield)
        {
            if (playerFleet.Count > 1)
            {
                int x = playerFleet.IndexOf(ship);
                if (x < playerFleet.Count - 1) nextShipToShield = playerFleet[x + 1];
                else nextShipToShield = playerFleet[0];
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
        
        int x = UnityEngine.Random.Range(0, 4);
        int v = UnityEngine.Random.Range(3, 8);

        EnemyFleetManager.instance.distributeResources(x, value);
    }

    public void checkActionsOfFleet() {
        foreach (PlayerShip ship in playerFleet) ship.checkActions();
        EnemyFleetManager.instance.checkActionsOfFleet();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
