using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject failPanel, finishPanel;

    private const float ActivateCanvasDelay = 3;

    private void Awake()
    {
        ActionController.Instance.AddMethodFail(Fail);
        ActionController.Instance.AddMethodFinish(Finish);
    }

    private void Finish()
    {
        StartCoroutine(ActivatePanel(finishPanel, true, ActivateCanvasDelay));
    }
    
    private void Fail()
    {
        StartCoroutine(ActivatePanel(failPanel, true, ActivateCanvasDelay));
    }
    
    private static IEnumerator ActivatePanel(GameObject panel, bool activate, float delay)
    {
        yield return new WaitForSeconds(delay);

        panel.SetActive(activate);
    }
}
