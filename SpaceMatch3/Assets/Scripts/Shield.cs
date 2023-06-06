using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [NonSerialized]
    public float shieldEnergy;
    private GameObject _gameObject;
    // Start is called before the first frame update
    void Start()
    {
        _gameObject = gameObject;
    }

    public void reduceShield(float value)
    {
        shieldEnergy -= value;
        if (shieldEnergy < 0) shieldEnergy = 0;
        if (shieldEnergy == 0) disactivateShield();
    }

    public void disactivateShield()
    {
        _gameObject.SetActive(false);
    }

}
