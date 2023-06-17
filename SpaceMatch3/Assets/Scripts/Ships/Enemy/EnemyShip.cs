using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : Ship
{
    public override bool canShot()
    {
        if (shotEnergy >= shotPower && shotPower <= energy && PlayerFleetManager.instance.playerFleet.Count > 0) return true;
        else return false;
    }

    public override void addToFleetManager()
    {
        EnemyFleetManager.instance.enemyFleet.Add(this);
    }
    public override void removeFromFleetManager()
    {
        EnemyFleetManager.instance.assignNextShipToEnergyIfThisDestroyed(this);
        EnemyFleetManager.instance.assignNextShipToHPIfThisDestroyed(this);
        EnemyFleetManager.instance.assignNextShipToShotIfThisDestroyed(this);
        EnemyFleetManager.instance.assignNextShipToShieldIfThisDestroyed(this);
        EnemyFleetManager.instance.enemyFleet.Remove(this);

    }
    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
