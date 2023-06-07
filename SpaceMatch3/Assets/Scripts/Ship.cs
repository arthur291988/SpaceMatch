using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.Mathematics;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
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
    public float energyMax;
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
    public float minShotTime;
    [NonSerialized]
    public float maxShotTime;

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

    public SpriteRenderer _spriteRendererOfLifeLine;
    [NonSerialized]
    public MaterialPropertyBlock matBlockOfLifeLineSprite;

    public SpriteRenderer _spriteRendererOfShieldLine;
    [NonSerialized]
    public MaterialPropertyBlock matBlockOfShieldLineSprite;

    public SpriteRenderer _spriteRendererOfEnergyLine;
    [NonSerialized]
    public MaterialPropertyBlock matBlockOfEnergyLineSprite;


    // Start is called before the first frame update
    void Start()
    {
    }

    public void StartSettings() {
        if (_gameObject == null) _gameObject = gameObject;
        if (_transform == null) _transform = _gameObject.transform;
        if (matBlockOfLifeLineSprite == null) matBlockOfLifeLineSprite = new MaterialPropertyBlock();
        if (matBlockOfShieldLineSprite == null) matBlockOfShieldLineSprite = new MaterialPropertyBlock();
        if (matBlockOfEnergyLineSprite == null) matBlockOfEnergyLineSprite = new MaterialPropertyBlock();

        shipPosition = (Vector2)_transform.position;
        addToFleetManager();
        accuracy = 0.2f;
        shotImpulse = 15;
        shotPower = 0.7f;
        shieldEnergyMax = 3;
        HPMax = 5;
        HP = HPMax;
        shieldCumulation = 0;
        energyMax = 7;
        energy = energyMax;
        minShotTime = 0.5f;
        maxShotTime = 1.5f;

        updateLifeLine();
        updateEnergyLine();
    }

    public virtual void addToFleetManager() { 
    
    }
    public virtual void removeFromFleetManager()
    {

    }

    private void updateLifeLine()
    {
        _spriteRendererOfLifeLine.GetPropertyBlock(matBlockOfLifeLineSprite);
        matBlockOfLifeLineSprite.SetFloat("_Fill", HP / HPMax);
        _spriteRendererOfLifeLine.SetPropertyBlock(matBlockOfLifeLineSprite);
    }
    private void updateShieldLine()
    {
        _spriteRendererOfShieldLine.GetPropertyBlock(matBlockOfShieldLineSprite);
        matBlockOfShieldLineSprite.SetFloat("_Fill", shieldCumulation / shieldEnergyMax);
        _spriteRendererOfShieldLine.SetPropertyBlock(matBlockOfShieldLineSprite);
    }
    private void updateEnergyLine()
    {
        _spriteRendererOfEnergyLine.GetPropertyBlock(matBlockOfEnergyLineSprite);
        matBlockOfEnergyLineSprite.SetFloat("_Fill", energy / energyMax);
        _spriteRendererOfEnergyLine.SetPropertyBlock(matBlockOfEnergyLineSprite);
    }


    public void reduceHP(float value) {
        HP -= value;
        updateLifeLine();
        if (HP < 0) HP = 0;
        if (HP == 0) disactivateShip();
    }

    public void consumeEnergy(float value) { 
        energy -= value;
        updateEnergyLine();
    }

    public void increaseEnergy(float value)
    {
        if (energy<energyMax) energy += value;
        if (energy > energyMax) energy = energyMax;
        updateEnergyLine();
    }

    public void disactivateShip()
    {
        removeFromFleetManager();
        _gameObject.SetActive(false);
    }

    public bool canShot() {
        if (shotEnergy >= shotPower && shotPower <= energy) return true;
        else return false;
    }

    public virtual void makeShot()
    {
        consumeEnergy(shotPower);
        shotEnergy -= shotPower;
        StartCoroutine (makeExtraShot());
    }
    public IEnumerator makeExtraShot()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(minShotTime, maxShotTime));
        if (canShot()) makeShot();
    }

    public void checkActions() {
        if (canShot()) makeShot();
        if (shieldCumulation >= shieldEnergyMax && shieldEnergyMax <= energy) activatePowerShiled(); //power shield activation is less important then shot
    }

    public void cumulateShiled(float value) {
        if (!shield.activeInHierarchy) shieldCumulation += value;
        updateShieldLine();
    }

    public void activatePowerShiled() {
        shieldClass.shieldEnergy = shieldEnergyMax;
        shieldClass.shieldEnergyMax = shieldEnergyMax;
        shieldCumulation = 0;
        consumeEnergy(shieldEnergyMax);
        shield.SetActive(true);
        shieldClass.updateShieldLine();
    }
    public void activatePowerShiledOnStart()
    {
        shieldClass.shieldEnergy = shieldEnergyMax;
        shieldClass.shieldEnergyMax = shieldEnergyMax;
        shield.SetActive(true);
        shieldClass.updateShieldLine();
    }


    public void healShield(float value)
    {
        if (shieldClass.shieldEnergy < shieldEnergyMax && value <= energy)
        {
            consumeEnergy(value);
            shieldClass.shieldEnergy += value;
            shieldClass.updateShieldLine();
        }
        if (shieldClass.shieldEnergy > shieldEnergyMax)
        {
            shieldClass.shieldEnergy = shieldEnergyMax;
            shieldClass.updateShieldLine();
        }
    }

    public void healHP(float value) {
        HP += value; 
        if (HP > HPMax) HP = HPMax;
        updateLifeLine();
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
