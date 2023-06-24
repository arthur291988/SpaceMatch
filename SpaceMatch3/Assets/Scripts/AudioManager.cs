using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField]
    private AudioSource flagShot;
    [SerializeField]
    private AudioSource cruisShot;
    [SerializeField]
    private AudioSource destrShot;
    [SerializeField]
    private AudioSource Explosion;
    [SerializeField]
    private AudioSource tileSound0;
    [SerializeField]
    private AudioSource tileSound1;

    private void Awake()
    {
        Instance = this;
    }

    public void shotSoundPlay(int index) {
        if (index == 0) destrShot.Play();
        else if (index == 1) cruisShot.Play();
        else flagShot.Play();
    }
    public void explosionPlay()
    {
        Explosion.Play();
    }
    public void tilePlay(int value)
    {
        if (value < 5) tileSound0.Play();
        else tileSound1.Play();
    }

}
