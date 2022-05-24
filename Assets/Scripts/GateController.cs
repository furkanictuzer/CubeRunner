using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    [SerializeField] private List<Gate> gates = new List<Gate>();
    
    public GameObject holePrefab;

    private void Awake()
    {
        GetGates();
    }

    public void UseGate()
    {
        foreach (var gate in gates)
        {
            gate.isUsed = true;
        }
    }

    private void GetGates()
    {
        foreach (Transform obj in transform)
        {
            gates.Add(obj.GetComponent<Gate>());
        }
    }
}
