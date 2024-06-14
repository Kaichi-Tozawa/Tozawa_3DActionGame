using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// UnityEvent�ɓo�^���ꂽ�v���n�u�ˑ��̏����̃C���X�^���X��
/// �V�[����̃I�u�W�F�N�g�ɍ����ւ���
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
            Debug.Log("����");
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
                "�C�x���g�ɒ�`���ꂽ�v���n�u�Ɠ����̃I�u�W�F�N�g���V�[���ɑ��݂��܂���");
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
