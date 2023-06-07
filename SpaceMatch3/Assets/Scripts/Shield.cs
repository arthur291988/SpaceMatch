using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [NonSerialized]
    public float shieldEnergy;
    [NonSerialized]
    public float shieldEnergyMax;
    private GameObject _gameObject;

    public SpriteRenderer _spriteRendererOfShieldLine;
    [NonSerialized]
    public MaterialPropertyBlock matBlockOfShieldLineSprite;


    // Start is called before the first frame update
    void Start()
    {
        _gameObject = gameObject;
    }

    private void OnEnable()
    {
        if (matBlockOfShieldLineSprite == null) matBlockOfShieldLineSprite = new MaterialPropertyBlock();
    }

    public void reduceShield(float value)
    {
        shieldEnergy -= value;
        if (shieldEnergy < 0) shieldEnergy = 0;
        updateShieldLine();
        if (shieldEnergy == 0) disactivateShield();
    }

    public void updateShieldLine()
    {
        _spriteRendererOfShieldLine.GetPropertyBlock(matBlockOfShieldLineSprite);
        matBlockOfShieldLineSprite.SetFloat("_Fill", shieldEnergy / shieldEnergyMax);
        _spriteRendererOfShieldLine.SetPropertyBlock(matBlockOfShieldLineSprite);
    }

    public void disactivateShield()
    {
        _gameObject.SetActive(false);
    }

}
