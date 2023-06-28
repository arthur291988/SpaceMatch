using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitchManager : MonoBehaviour
{
    public static void LoadMenuScene()
    {
        Loader.Load(0);
    }
    public static void LoadBattleScene()
    {
        Loader.Load(1);
    }
}
