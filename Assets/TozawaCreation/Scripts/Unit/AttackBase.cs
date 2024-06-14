using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Attack
{
    /// <summary>
    /// ������U���̊�b�ƂȂ�e�N���X
    /// </summary>
    public class AttackBase : MonoBehaviour
    {
        [SerializeField, Header("�_���[�W�ƂȂ�U����")] int _attackPower = 0;
        [SerializeField, Header("�m�b�N�o�b�N������Ռ���")] float _impactPower;
        [SerializeField, Header("�U�����q�b�g�����Ƃ��̃G�t�F�N�g")] GameObject _hitEffect;
        string _ownTag;
        protected void Awake()
        {
            _ownTag = this.gameObject.tag;
        }
        protected float AttackPower
        {
            get { return _attackPower; }
        }
        protected float ImpactPower
        {
            get { return _impactPower; }
        }
        protected GameObject HitEffect
        {
            get { return _hitEffect; }
        }
        protected string OwnTag
        {
            get { return _ownTag; }
        }
    }
}
