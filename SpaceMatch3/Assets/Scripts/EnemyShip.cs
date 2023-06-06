using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : Ship
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void makeShot()
    {
        ObjectPulledList = ObjectPuller.current.GetEnemyShotPullList();
        ObjectPulled = ObjectPuller.current.GetGameObjectFromPull(ObjectPulledList);
        ObjectPulled.transform.position = shipPosition;
        ObjectPulled.GetComponent<EnemyShot>()._harm = shotPower;

        PlayerShip shipToAttack = PlayerFleetManager.instance.playerFleet.Count == 1 ? PlayerFleetManager.instance.playerFleet[0] :
                PlayerFleetManager.instance.playerFleet[Random.Range(0, PlayerFleetManager.instance.playerFleet.Count)];

        attackDirection = shipToAttack.shipPosition;

        attackDirection -= shipPosition;
        attackDirection = RotateAttackVector(attackDirection, Random.Range(-accuracy, accuracy));
        ObjectPulled.SetActive(true);

        ObjectPulled.GetComponent<Rigidbody2D>().AddForce(attackDirection.normalized * shotImpulse, ForceMode2D.Impulse);


        base.makeShot();
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
