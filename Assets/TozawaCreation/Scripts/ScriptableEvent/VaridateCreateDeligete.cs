using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

public class VaridateCreateDeligete : MonoBehaviour
{
    [SerializeField] UnityEvent _testEvent;
    UnityAction _testAction;
    void Start()
    {
        _testEvent.SetPersistentListenerState(0, UnityEventCallState.RuntimeOnly);
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            var x = _testEvent.GetPersistentTarget(0).GetType().GetMethod(_testEvent.GetPersistentMethodName(0));
            Debug.Log(x);
            var y = UnityEvent.GetValidMethodInfo(_testEvent.GetPersistentTarget(0), _testEvent.GetPersistentMethodName(0), new System.Type[] { });
            Debug.Log(y);
        }
    }
    void ShowLog()
    {
        Debug.Log(_testEvent.GetPersistentTarget(0).name);
        Debug.Log(_testEvent.GetPersistentMethodName(0));
        Debug.Log(_testEvent.GetPersistentListenerState(0));
        Debug.Log(_testEvent.GetPersistentEventCount());
        Debug.Log(_testAction.GetMethodInfo());
        Debug.Log("");
    }
    void Test()
    {

    }
}
