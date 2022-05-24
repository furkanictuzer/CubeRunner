using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GateInfoController : MonoBehaviour
{
    private Gate _gate;
    
    private TextMeshProUGUI _gateInfo;

    private void Awake()
    {
        _gate = GetComponent<Gate>();
        
        _gateInfo = GetComponentInChildren<TextMeshProUGUI>();

        SetInfoText(_gate.neededCubeCount);
    }

    private void SetInfoText(int num)
    {
        _gateInfo.text = num.ToString();
    }
}
