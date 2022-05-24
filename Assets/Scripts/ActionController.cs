using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoSingleton<ActionController>
{
    private Action _fail, _finish;

    public void OnFail()
    {
        _fail?.Invoke();
    }

    public void OnFinish()
    {
        _finish?.Invoke();
    }

    public void AddMethodFail(params Action[] methods)
    {
        foreach (var method in methods)
            _fail += method;
    }
    
    public void AddMethodFinish(params Action[] methods)
    {
        foreach (var method in methods)
            _finish += method;
    }
}
