using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonData : MonoBehaviour
{
    public static CommonData Instance { get; private set; }
    public Camera _camera;
    [NonSerialized]
    public float vertScreenSize;
    [NonSerialized]
    public float horisScreenSize;

    void Awake() { 
        Instance = this;

        //determine the sizes of view screen
        vertScreenSize = _camera.orthographicSize * 2;
        horisScreenSize = vertScreenSize * Screen.width / Screen.height;
    }
}
