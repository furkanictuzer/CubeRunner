using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.GetComponent<StackController>())
        {
            ActionController.Instance.OnFinish();
        }
    }
}
