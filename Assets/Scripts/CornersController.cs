using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class CornersController : MonoSingleton<CornersController>
{
    private List<Transform> _corners = new List<Transform>();
    private List<Transform> _touchedCorners = new List<Transform>();


    [CanBeNull] //[HideInInspector]
    public Transform touchedLeftCorner, touchedRightCorner;

    private void Awake()
    {
        GetCornerInChildren();
    }

    private void GetCornerInChildren()
    {
        foreach (Transform child in transform)
        {
            _corners.Add(child);
        }
    }

    private void SelectLeftRightCorner(IReadOnlyList<Transform> corners)
    {
        if (corners.Count <= 0)
        {
            touchedLeftCorner = null;
            touchedRightCorner = null;
            
            return;
        }

        if (corners.Count == 1)
        {
            touchedLeftCorner = corners[0];
            touchedRightCorner = corners[0];
            
            return;
        }

        var leftCorner = corners?[0].transform == null ? null : corners[0].transform;
        var rightCorner = corners?[0].transform == null ? null : corners[0].transform;

        foreach (var corner in corners)
        {
            if (leftCorner != null && corner.position.x < leftCorner.position.x)
            {
                leftCorner = corner;
            }

            if (rightCorner != null && corner.position.x>rightCorner.position.x)
            {
                rightCorner = corner;
            }
        }

        touchedLeftCorner = leftCorner;
        touchedRightCorner = rightCorner;
    }

    public void AddTouchedCorner(Transform corner)
    {
        _touchedCorners.Add(corner);
        
        SelectLeftRightCorner(_touchedCorners);
    }

    public void RemoveTouchedCorner(Transform corner)
    {
        _touchedCorners.Remove(corner);
        
        SelectLeftRightCorner(_touchedCorners);
    }
}
