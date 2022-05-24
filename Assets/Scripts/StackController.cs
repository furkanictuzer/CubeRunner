using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StackController : MonoSingleton<StackController>
{
    [SerializeField] private List<Transform> collectedCubes = new List<Transform>();
    [Space]
    [SerializeField] private Transform stackGarbage;
    [Space]
    [SerializeField] private Material stackMaterial, unstackMaterial;

    private Color _stackColor;
    
    private const float StackOffset = 1f;
    
    private const float HSVOffset = 5f / 360f;

    private float _lastHSV;

    private void Awake()
    {
        _stackColor = stackMaterial.color;
    }

    private void AlignCube(Transform obj)
    {
        obj.parent = transform;

        obj.localPosition = new Vector3(0, 0, (collectedCubes.Count - 1) * StackOffset);

        //obj.DOLocalMove(new Vector3(0, 0, (collectedCubes.Count - 1) * StackOffset), AlignAnimTime);
    }
    
    public void CollectCube(CollectableObject collectableObject, Transform objTransform)
    {
        collectableObject.target = collectedCubes[collectedCubes.Count - 1];
        
        collectedCubes.Add(objTransform);

        AlignCube(objTransform);
    }

    public void BreakStack(Transform objTransform)
    {
        var diff = collectedCubes.Count - collectedCubes.IndexOf(objTransform);
        
        for (var i = 0; i < diff; i++)
        {
            var obj = collectedCubes[collectedCubes.Count-1];
            var collectableObject = obj.GetComponent<CollectableObject>();
            
            _lastHSV += HSVOffset;

            if (collectableObject == null)
                ActionController.Instance.OnFail();
            
            
            obj.gameObject.SetActive(false);

            obj.parent = stackGarbage;
            
            collectableObject.target = null;
            
            collectedCubes.Remove(obj);
            
            obj.gameObject.SetActive(false);
        }
        
        ControlStack();
    }

    public List<GameObject> TakeCubes(int takenCubeNum)
    {
        var list = new List<GameObject>();

        if (takenCubeNum >= collectedCubes.Count)
            takenCubeNum = collectedCubes.Count;
        
        
        for (var i = 0; i < takenCubeNum; i++)
        {
            list.Add(collectedCubes[collectedCubes.Count - 1].gameObject);

            collectedCubes[collectedCubes.Count - 1].parent = stackGarbage;

            collectedCubes.RemoveAt(collectedCubes.Count - 1);
        }

        ControlStack();
        
        return list;
    }

    public void ChangeMaterial(GameObject obj, bool isStack)
    {
        var rend=obj.GetComponent<Renderer>();
        
        rend.material = isStack ? stackMaterial : unstackMaterial;

        Color.RGBToHSV(_stackColor,out _,out var s,out var v);

        _lastHSV += HSVOffset;

        rend.material.color = Color.HSVToRGB(_lastHSV, s, v);
    }

    public bool ControlStack()
    {
        if (collectedCubes.Count <= 0)
        {
            ActionController.Instance.OnFail();

            return true;
        }

        return false;
    }

}
