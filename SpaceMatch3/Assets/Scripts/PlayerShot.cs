using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class PlayerShot : Shot
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyShip>(out EnemyShip enemyShip))
        {
            enemyShip.reduceHP(_harm);
            disactivateShot();
        }

        if (collision.gameObject.TryGetComponent<EnemyShot>(out EnemyShot shot))
        {
            disactivateShot();
        }
        if (collision.gameObject.TryGetComponent<Shield>(out Shield shield))
        {
            float harmBeforeCollision = _harm;
            reduceHarm(shield.shieldEnergy);
            shield.reduceShield(harmBeforeCollision);
            if (_harm <= 0) disactivateShot();
        }

        _trailRenderer.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
