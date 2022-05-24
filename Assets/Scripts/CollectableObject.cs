using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : MonoBehaviour
{
    private const float LerpSpeed = 20;

    private bool _isCollectable = true;
    
    public Transform target;
    
    public bool collected;

    private void Update()
    {
        if (!collected || target == null) return;
        
        BehaveLikeTarget();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isCollectable) return;

            if (other.GetComponent<Cube>() && !collected)
        {
            Collect();
        }
        else if (other.GetComponent<Obstacle>() && collected)
        {
            Break();
        }
    }
    
    private void Collect()
    {
        StackController.Instance.CollectCube(this, transform);

        StackController.Instance.ChangeMaterial(gameObject,true);
        
        collected = true;
    }

    private void Break()
    {
        StackController.Instance.BreakStack(transform);

        collected = false;
    }

    private void BehaveLikeTarget()
    {
        var obj = transform;
        var localPos = obj.localPosition;
        var pos = Vector3.Lerp(localPos, target.localPosition, Time.deltaTime * LerpSpeed);

        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, Time.deltaTime * LerpSpeed);
        
        pos.z = localPos.z;

        obj.localPosition = pos;
    }

    public void TakeThis(bool collectable)
    {
        target = null;
        
        _isCollectable = collectable;
    }

    
}
