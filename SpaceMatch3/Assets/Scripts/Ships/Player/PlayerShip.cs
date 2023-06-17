using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerShip : Ship
{
    public override bool canShot()
    {
        if (shotEnergy >= shotPower && shotPower <= energy && EnemyFleetManager.instance.enemyFleet.Count>0) return true;
        else return false;
    }

    public override void addToFleetManager()
    {
        PlayerFleetManager.instance.playerFleet.Add(this);
    }
    public override void removeFromFleetManager()
    {
        PlayerFleetManager.instance.assignNextShipToEnergyIfThisDestroyed(this);
        PlayerFleetManager.instance.assignNextShipToHPIfThisDestroyed(this);
        PlayerFleetManager.instance.assignNextShipToShotIfThisDestroyed(this);
        PlayerFleetManager.instance.assignNextShipToShieldIfThisDestroyed(this);
        PlayerFleetManager.instance.playerFleet.Remove(this);

    }
}
