
using UnityEngine;

public class DisactivateEffect : MonoBehaviour
{
    //disactivates any effect prefab after it was pulled from Object Puller
    private GameObject _GO;

    private void OnEnable()
    {
        if (_GO == null) _GO = gameObject;
        Invoke("setFalseGameObj", 1);
    }

    private void setFalseGameObj()
    {
        _GO.SetActive(false);
    }
}
