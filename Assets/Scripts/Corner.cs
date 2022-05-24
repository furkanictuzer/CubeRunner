using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corner : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Ground>())
        {
            CornersController.Instance.AddTouchedCorner(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Ground>())
        {
            CornersController.Instance.RemoveTouchedCorner(transform);
        }
    }
}
