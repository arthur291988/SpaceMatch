using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFleetManager : MonoBehaviour
{
    [NonSerialized]
    public List<Ship> playerFleet;


    [NonSerialized]
    public Ship nextShipToEnergy;
    [NonSerialized]
    public Ship nextShipToShot;
    [NonSerialized]
    public Ship nextShipToHP;
    [NonSerialized]
    public Ship nextShipToShield;

    

    public static PlayerFleetManager instance;

    private float HPAddValue;
    private float ShieldAddValue;

    private void Awake()
    {
        instance = this;


    }

    private void OnEnable()
    {
        playerFleet = new List<Ship>();
    }

    //void Start()
    //{
    //    Debug.Log(CommonData.Instance.vertScreenSize / 2 + 1);
    //    Debug.Log(CommonData.Instance.vertScreenSize / -2 - 1);

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

    public void assignNextShipToEnergyIfThisDestroyed(Ship ship)
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

    public void assignNextShipToHPIfThisDestroyed(Ship ship)
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

    public void assignNextShipToShotIfThisDestroyed(Ship ship)
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

    public void assignNextShipToShieldIfThisDestroyed(Ship ship)
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

    public void distributeResources(int index, int value, int comboValue)
    {
        //shot
        if (index == 0)
        {
            for (int i = 0; i < value; i++)
            {
                nextShipToShot.increaseShotPower();
                assignNextShipToShot();
            }
        }
        //energy
        else if (index == 1)
        {
            for (int i = 0; i < value; i++)
            {
                nextShipToEnergy.increaseEnergy(1);
                assignNextShipToEnergy();
            }
        }
        //shield
        else if (index == 2)
        {
            for (int i = 0; i < value; i++)
            {
                if (nextShipToShield.shield.activeInHierarchy) nextShipToShield.healShield(ShieldAddValue);
                else nextShipToShield.cumulateShiled(ShieldAddValue);

                assignNextShipToShield();
            }
        }
        //HP
        else if (index == 3)
        {
            for (int i = 0; i < value; i++)
            {
                nextShipToHP.healHP(HPAddValue);
                assignNextShipToHP();
            }
        }
        //aiming add accures randomely
        else if (index == 4)
        {
            if (playerFleet.Count > 0) {
                for (int i = 0; i < value; i++)
                {
                    playerFleet[UnityEngine.Random.Range(0, playerFleet.Count)].increaseAimingCount();
                }
            }
        }
        if (comboValue > 3) processCombo(index, comboValue);
    }

    public void processCombo(int index, int comboValue) {
        if (comboValue == 4) {
            distributeResources(UnityEngine.Random.Range(0,5),2,0); //0 is default, 2 meanse two additional random resource
        }

        //0 - shot, 1 - energy, 2 - shield, 3 - HP 
        if (comboValue == 5) {
            if (index == 0)
            {
                distributeResources(4, 3, 0); //4 is aim, 3 - value, 0 is default no combo call, 
                distributeResources(1, 3, 0); //1 is energy, 3 - value, 0 is default no combo call, 
                distributeResources(0, 2, 0); //0 is shot, 2 - value, 0 is default no combo call, 
            }
            if (index == 1)
            {
                distributeResources(2, 3, 0); 
                distributeResources(4, 1, 0); 
                distributeResources(0, 2, 0); 
            }
            if (index == 2)
            {
                distributeResources(1, 3, 0); 
                distributeResources(3, 3, 0);  
                distributeResources(4, 1, 0); 
            }
            if (index == 3)
            {
                distributeResources(1, 3, 0); 
                distributeResources(2, 3, 0); 
                distributeResources(4, 1, 0); 
            }
        }
        if (comboValue == 6)
        {
            if (index == 0)
            {
                distributeResources(4, 5, 0); 
                distributeResources(1, 4, 0); 
                distributeResources(0, 2, 0);  
                distributeResources(3, 2, 0);
            }
            if (index == 1)
            {
                distributeResources(2, 4, 0);
                distributeResources(0, 3, 0);
                distributeResources(1, 2, 0);
                distributeResources(3, 2, 0);
                distributeResources(4, 2, 0);
            }
            if (index == 2)
            {
                distributeResources(3, 5, 0);
                distributeResources(1, 3, 0);
                distributeResources(4, 2, 0);
                distributeResources(0, 2, 0);
            }
            if (index == 3)
            {
                distributeResources(1, 3, 0);
                distributeResources(2, 3, 0);
                distributeResources(0, 2, 0);
                distributeResources(4, 2, 0);
            }
        }
        if (comboValue == 7)
        {
            if (index == 0)
            {
                distributeResources(4, 7, 0);
                distributeResources(1, 6, 0);
                distributeResources(0, 3, 0);
                distributeResources(3, 3, 0);
            }
            if (index == 1)
            {
                distributeResources(2, 6, 0);
                distributeResources(0, 5, 0);
                distributeResources(1, 3, 0);
                distributeResources(3, 2, 0);
                distributeResources(4, 3, 0);
            }
            if (index == 2)
            {
                distributeResources(3, 7, 0);
                distributeResources(1, 5, 0);
                distributeResources(4, 3, 0);
                distributeResources(0, 3, 0);
            }
            if (index == 3)
            {
                distributeResources(1, 5, 0);
                distributeResources(2, 5, 0);
                distributeResources(0, 4, 0);
                distributeResources(4, 3, 0);
            }
        }
    }

    public void checkActionsOfFleet() {
        foreach (Ship ship in playerFleet) ship.checkActions();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
