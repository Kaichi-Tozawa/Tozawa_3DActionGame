using System;
using UnityEngine;
[Serializable]
public class InterfaceMediary<TInterface>
{
    [SerializeField,Header("�擾�������C���^�[�t�F�[�X���A�^�b�`���Ă�������")] GameObject _objectI;
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
