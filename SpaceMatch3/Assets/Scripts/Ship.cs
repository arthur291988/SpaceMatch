using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Ship : MonoBehaviour
{
    [NonSerialized]
    public float HP;
    [NonSerialized]
    public float HPMax;
    [NonSerialized]
    public float energy;
    [NonSerialized]
    public float shotEnergy;
    [NonSerialized]
    public float shotPower;
    [NonSerialized]
    public GameObject _gameObject;
    [NonSerialized]
    public Transform _transform;
    [NonSerialized]
    public Vector2 shipPosition;
    [NonSerialized]
    public float shieldEnergyMax;
    [NonSerialized]
    public float shieldCumulation;

    [NonSerialized]
    public Vector2 attackDirection;

    [NonSerialized]
    public GameObject ObjectPulled;
    [NonSerialized]
    public List<GameObject> ObjectPulledList;

    [NonSerialized]
    public float accuracy;
    [NonSerialized]
    public float shotImpulse;

    public GameObject shield;
    public Shield shieldClass;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void StartSettings() {
        if (_gameObject == null) _gameObject = gameObject;
        if (_transform == null) _transform = _gameObject.transform;
        shipPosition = (Vector2)_transform.position;
        addToFleetManager();
        accuracy = 0.2f;
        shotImpulse = 15;
        shotPower = 0.7f;
        shieldEnergyMax = 3;
        HPMax = 5;
        HP = HPMax;
        shieldCumulation = 0;
    }

    public virtual void addToFleetManager() { 
    
    }
    public virtual void removeFromFleetManager()
    {

    }

    public void reduceHP(float value) {
        HP -= value;
        if (HP < 0) HP = 0;
        if (HP == 0) disactivateShip();
    }

    

    public void disactivateShip()
    {
        removeFromFleetManager();
        _gameObject.SetActive(false);
    }

    public virtual void makeShot()
    {
        energy -= shotPower;
        shotEnergy -= shotPower;
    }

    public void checkActions() {
        if (shotEnergy >= shotPower && shotPower <=energy) makeShot();
        if (shieldCumulation >= shieldEnergyMax && shieldEnergyMax <= energy) activatePowerShiled(); //power shield activation is less important then shot
    }

    public void cumulateShiled(float value) {
        if (!shield.activeInHierarchy) shieldCumulation += value;
    }

    public void activatePowerShiled() {
        shieldClass.shieldEnergy = shieldEnergyMax;
        shieldCumulation = 0;
        energy -= shieldEnergyMax;
        shield.SetActive(true);
    }

    public void healShield(float value)
    {
        if (shieldClass.shieldEnergy < shieldEnergyMax && value <= energy)
        {
            energy -= value;
            shieldClass.shieldEnergy += value;
        }
        if (shieldClass.shieldEnergy > shieldEnergyMax) shieldClass.shieldEnergy = shieldEnergyMax;
    }

    public void healHP(float value) {
        HP += value; 
        if (HP > HPMax) HP = HPMax;
    }

    //Rotates the attack vector to add some randomness
    public Vector2 RotateAttackVector(Vector2 attackDirection, float delta)
    {
        return new Vector2(
            attackDirection.x * Mathf.Cos(delta) - attackDirection.y * Mathf.Sin(delta),
            attackDirection.x * Mathf.Sin(delta) + attackDirection.y * Mathf.Cos(delta)
        );
    }

}
