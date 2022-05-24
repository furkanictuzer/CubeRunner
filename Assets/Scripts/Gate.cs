using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private List<GameObject> _takenCubes = new List<GameObject>();

    private List<Transform> _holes = new List<Transform>();

    private GateController _gateController;

    private const float HoleZOffset = 1.5f;

    private const float TakeCubeAnimTime = 1.5f;
    private const float GoPosAnimTime = 0.5f;

    public bool isUsed;
    
    public int neededCubeCount;
    private void Awake()
    {
        _gateController = transform.parent.GetComponent<GateController>();
        
        CreateHoles();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.GetComponent<StackController>() && !isUsed)
        {
            TakeCubes();
        }
    }

    private void CreateHoles()
    {
        for (var i = 0; i < neededCubeCount; i++)
        {
            var obj = Instantiate(_gateController.holePrefab, transform, true);
            var pos = transform.position;

            obj.transform.position = new Vector3(pos.x, 0.25f, pos.z + 1 + i * HoleZOffset);

            _holes.Add(obj.transform);
        }
    }

    private void TakeCubes()
    {
        StartCoroutine(TakeCubesCoroutine());
    }

    private IEnumerator TakeCubesCoroutine()
    {
        MoveForward.Instance.SetSpeed(0);
        
        _gateController.UseGate();

        _takenCubes = StackController.Instance.TakeCubes(neededCubeCount);

        StartCoroutine(TakeCubesThePosCoroutine(_takenCubes));

        yield return new WaitForSeconds(TakeCubeAnimTime);

        var fail = StackController.Instance.ControlStack();

        if (!fail)
        {
            MoveForward.Instance.SetSpeed();
        }
    }

    private IEnumerator TakeCubesThePosCoroutine(IReadOnlyList<GameObject> cubes)
    {
        for (var i = 0; i < cubes.Count; i++)
        {
            cubes[i].GetComponent<CollectableObject>().TakeThis(false);
            
            cubes[i].transform.parent = _holes[i];

            cubes[i].transform.DOMove(_holes[i].position, GoPosAnimTime);
            cubes[i].transform.DORotate(new Vector3(90, 0, 0), GoPosAnimTime);
                
            yield return new WaitForSeconds(GoPosAnimTime * 0.8f);
        }
    }
    
    
}
