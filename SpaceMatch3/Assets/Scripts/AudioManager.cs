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

    private void Awake()
    {
        Instance = this;
    }

    public void shotSoundPlay(int index) {
        if (index == 0) destrShot.Play();
        else if (index == 1) cruisShot.Play();
        else flagShot.Play();
    }

}
