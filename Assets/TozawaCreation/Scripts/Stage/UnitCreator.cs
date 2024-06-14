using GameSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class UnitCreator : MonoBehaviour,IPause,IUnitCreator
{

    [SerializeField, Header("生成するUnit")] GameObject _unit;
    [SerializeField, Header("生成する場所")] Transform _createPos;
    [SerializeField, Header("生成物に付与したいOnDestroyコールバック処理")] UnityEvent _onDestroyCallBack;
    [SerializeField, Header("生成する間隔")] float _creatInterval;
    [SerializeField, Header("定期的にユニットを生成するか否か")] bool _isPeriodicallyCreate;
    Action _action;


    private void OnEnable()
    {
        StartCoroutine(CreateUnitVerTimeCorutine(_creatInterval));
        ConvertEventToAction();
    }

    private void ConvertEventToAction()
    {
        int count = _onDestroyCallBack.GetPersistentEventCount();
        for (int i = 0; i < count; ++i)
        {
            _action += (Action)System.Delegate.
                CreateDelegate(typeof(Action),_onDestroyCallBack.GetPersistentTarget(i), _onDestroyCallBack.GetPersistentMethodName(i));
        }
    }

    IEnumerator CreateUnitVerTimeCorutine(float interval)
    {
        while(_isPeriodicallyCreate)
        {
            yield return new WaitForSeconds(interval);
            CreateUnit();
        }
    }
    /// <summary>
    /// ユニットを生成
    /// </summary>
    public void CreateUnit()
    {
        if (!_createPos)
        {
            _createPos = this.transform;
        }
        var unit = Instantiate(_unit, _createPos.position, Quaternion.identity);
        if(_action != null)
        {
            unit.AddOnDestroyCallBack(_action);
            Debug.Log("AddCalback"+_action);
        }
        if(unit.TryGetComponent<INPCComander>(out var c))
        {
            c.SetPriorityTarget(GameObject.Find("Castle"));
        }
    }

    public void Pause()
    {
        _isPeriodicallyCreate = false;
    }

    public void Reboot()
    {
        _isPeriodicallyCreate = true;
    }
}
