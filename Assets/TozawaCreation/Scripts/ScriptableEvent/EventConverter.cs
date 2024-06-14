using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// UnityEventに登録されたプレハブ依存の処理のインスタンスを
/// シーン上のオブジェクトに差し替える
/// </summary>
public class EventConverter : MonoBehaviour
{
    [SerializeField] UnityEvent _onPrefabMethods;
    Action _action;
    UnityAction  _unityaction;
    delegate void TestDelegate(UnityAction action);
    TestDelegate _testDelegate;
    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Convert();
            _action.Invoke();
            Debug.Log("完了");
        }
    }
    void Convert()
    {
        int length = _onPrefabMethods.GetPersistentEventCount();
        for (int i = 0; i < length; ++i)
        {
            _action = (Action)System.Delegate.
                CreateDelegate(typeof(Action), SceneInstance(i), EventMethodInfo(i));
        }
        
    }
    object SceneInstance(int count)
    {
        try
        {
            var sceneobj = GameObject.Find(_onPrefabMethods.GetPersistentTarget(count).name);
            var comp = sceneobj.GetComponent(EventMethodInfo(count).DeclaringType);
            UnityAction action = Delegate.CreateDelegate
                (typeof(UnityAction), comp, EventMethodInfo(count))as UnityAction;
            return action.Target;
        }
        catch (Exception e) 
        {
            Debug.Log(e + "\n" + 
                "イベントに定義されたプレハブと同名のオブジェクトがシーンに存在しません");
            return null;
        }
    }
    

    MethodInfo EventMethodInfo(int count)
    {
        var info = _onPrefabMethods.GetPersistentTarget(count).GetType()
            .GetMethod(_onPrefabMethods.GetPersistentMethodName(count));
        return info;
    }
}
