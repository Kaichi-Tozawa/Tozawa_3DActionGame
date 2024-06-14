using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class OnDestroyCallBack : MonoBehaviour
{
    Action _onDestroy;
    /// <summary>
    /// ŠO•”‚©‚çobj‚ª”j‰ó‚³‚ê‚½‚É‚µ‚Ä‚Ù‚µ‚¢callback‚ğİ’è‚·‚é
    /// </summary>
    /// <param name="callback"></param>
    /// <param name="obj"></param>
    public static void AddOnDestroyCallBack(Action callback,GameObject obj)
    {
        OnDestroyCallBack ondestroy = obj.GetComponent<OnDestroyCallBack>() ?? obj.AddComponent<OnDestroyCallBack>();
        ondestroy._onDestroy += callback;
    }
    private void OnDestroy()
    {
        if(_onDestroy != null)
        {
            _onDestroy.Invoke();
        }
    }
}

public static class OndestroyCallBackExtensions
{
    /// <summary>
    /// ŠO•”‚©‚çobj‚ª”j‰ó‚³‚ê‚½‚É‚µ‚Ä‚Ù‚µ‚¢callback‚ğİ’è‚·‚é
    /// </summary>
    /// <param name="callback"></param>
    /// <param name="obj"></param>
    public static void AddOnDestroyCallBack(this GameObject obj, Action callback)
    {
        OnDestroyCallBack.AddOnDestroyCallBack(callback, obj);
    }
}