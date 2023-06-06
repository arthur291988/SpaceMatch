using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    [HideInInspector]
    public Transform _shotTransform;
    [HideInInspector]
    public Rigidbody2D _shotRigidBody2D;
    [HideInInspector]
    public GameObject _gameObject;
    [HideInInspector]
    public TrailRenderer _trailRenderer;
    [HideInInspector]
    public float _harm;



    private void Awake()
    {
        _trailRenderer = gameObject.GetComponent<TrailRenderer>();
    }

    //private void Start()
    //{
    //}

    private void OnEnable()
    {
        _trailRenderer.Clear();
        if (_gameObject == null) _gameObject = gameObject;
        if (_shotTransform == null) _shotTransform = transform;
        if (_shotRigidBody2D == null) _shotRigidBody2D = GetComponent<Rigidbody2D>();
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    public void reduceHarm(float harm) {
        _harm -= harm;
        if (harm <= 0) disactivateShot();
    }

    public void disactivateShot()
    {
        _gameObject.SetActive(false);

    }


    // Update is called once per frame
    void Update()
    {
        if (_shotTransform.position.y > CommonData.Instance.vertScreenSize / 2 + 1 || _shotTransform.position.y < -CommonData.Instance.vertScreenSize / 2 - 1)
        {
            _gameObject.SetActive(false);
        }
    }
}
