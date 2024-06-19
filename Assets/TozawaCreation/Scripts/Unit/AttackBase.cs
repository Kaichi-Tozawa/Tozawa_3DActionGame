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
        [SerializeField, Header("�_���[�W�ƂȂ�U����")] int _damage = 0;
        [SerializeField, Header("�m�b�N�o�b�N������Ռ���")] float _impact;
        [SerializeField, Header("�U�����q�b�g�����Ƃ��̃G�t�F�N�g")] GameObject _hitEffect;
        string _ownTag;
        protected void Awake()
        {
            _ownTag = this.gameObject.tag;
        }

        private protected virtual void AttackEvent(){ }
        public void OnAttackEvent() 
        {
            AttackEvent();
        }

        protected int DamagePower()
        {
            return _damage;
        }
        protected float ImpactPower()
        {
           return _impact;
        }
        protected GameObject HitEffect()
        {
            return _hitEffect;
        }
        protected string OwnTag()
        {
            return _ownTag;
        }

    }
}
