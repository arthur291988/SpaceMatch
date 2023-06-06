using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Ship
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void makeShot()
    {
        ObjectPulledList = ObjectPuller.current.GetPlayerShotPullList();
        ObjectPulled = ObjectPuller.current.GetGameObjectFromPull(ObjectPulledList);
        ObjectPulled.transform.position = shipPosition;
        ObjectPulled.GetComponent<PlayerShot>()._harm = shotPower;


        EnemyShip shipToAttack = EnemyFleetManager.instance.enemyFleet.Count == 1 ? EnemyFleetManager.instance.enemyFleet[0] :
                EnemyFleetManager.instance.enemyFleet[Random.Range(0, EnemyFleetManager.instance.enemyFleet.Count)];


        attackDirection = shipToAttack.shipPosition;

        attackDirection -= shipPosition;
        attackDirection = RotateAttackVector(attackDirection, Random.Range(-accuracy, accuracy));
        ObjectPulled.SetActive(true);

        ObjectPulled.GetComponent<Rigidbody2D>().AddForce(attackDirection.normalized * shotImpulse, ForceMode2D.Impulse);

        base.makeShot();
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
