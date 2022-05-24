using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoveForward : MonoSingleton<MoveForward>
{
    [SerializeField] private float speed = 5;

    private float _initSpeed;

    private void Awake()
    {
        _initSpeed = speed;
        
        ActionController.Instance.AddMethodFail(StopMove);
        ActionController.Instance.AddMethodFinish(StopMove);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * (Time.deltaTime * speed));
    }

    private void StopMove()
    {
        SetSpeed(0);
    }

    public void SetSpeed(float newSpeed = -1)
    {
        if (newSpeed < 0)
            newSpeed = _initSpeed;
        
        speed = newSpeed;
    }
}
