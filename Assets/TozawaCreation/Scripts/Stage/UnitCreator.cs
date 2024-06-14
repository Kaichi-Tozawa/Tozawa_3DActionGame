using GameSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class UnitCreator : MonoBehaviour,IPause,IUnitCreator
{

    [SerializeField, Header("��������Unit")] GameObject _unit;
    [SerializeField, Header("��������ꏊ")] Transform _createPos;
    [SerializeField, Header("�������ɕt�^������OnDestroy�R�[���o�b�N����")] UnityEvent _onDestroyCallBack;
    [SerializeField, Header("��������Ԋu")] float _creatInterval;
    [SerializeField, Header("����I�Ƀ��j�b�g�𐶐����邩�ۂ�")] bool _isPeriodicallyCreate;
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
    /// ���j�b�g�𐶐�
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
