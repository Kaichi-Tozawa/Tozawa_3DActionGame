using System;
using UnityEngine;
[Serializable]
public class InterfaceMediary<TInterface>
{
    [SerializeField,Header("取得したいインターフェースをアタッチしてください")] GameObject _objectI;
    TInterface _interface;
    public TInterface Interface()
    {
        if(_objectI)
        {
            _objectI.TryGetComponent<TInterface>(out _interface);
        }
        return _interface;
    }
}
