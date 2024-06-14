using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Attack
{
    /// <summary>
    /// �ߐڍU��
    /// </summary>
    public class MeleeAttack : AttackBase
    {
        [SerializeField, Header("�U���͈͋��̂̒��S�_")] Vector3 _attackRangeCenter;
        [SerializeField, Header("�U���͈͋��̂̔��a")] float _attackRangeRadius;
        IHealth _targetIH;
        protected float AttackRangeRadius
        {
            get { return _attackRangeRadius; }
        }
        /// <summary>
        /// �S�Ă̋ߐڍU����Physics.Overlap�ōU���������s���B����̓A�j���[�V�����C�x���g����Ă΂��֐��ł���
        /// </summary>
        public virtual void AttackEvent()
        {
            foreach (var target in Physics.OverlapSphere(GetAttackRangeCenter(), _attackRangeRadius)
                .Where(x =>
                !x.gameObject.CompareTag(base.OwnTag)
                && x.TryGetComponent<IHealth>(out _targetIH)))
            {
                var _targetRB = target.GetComponent<Rigidbody>();
                if (_targetRB != null) 
                {
                    _targetRB.AddForce(this.transform.forward * base.ImpactPower, ForceMode.Impulse);
                }
                _targetIH.TakeDamage((int)base.AttackPower);
                Instantiate(base.HitEffect, GetAttackRangeCenter(), this.transform.rotation);
            }
        }
        /// <summary>
        /// �U���͈͂̒��S�_�����
        /// </summary>
        /// <returns>�U���͈͂̒��S�_(Vector3)</returns>
        protected Vector3 GetAttackRangeCenter()
        {
            Vector3 center = this.transform.position + this.transform.forward * _attackRangeCenter.z
                + this.transform.up * _attackRangeCenter.y
                + this.transform.right * _attackRangeCenter.x;
            return center;
        }

        /// <summary>
        /// �U���͈͂�Ԃ����ŃV�[���r���[�ɕ\������B�ߐڍU�������S�Ẵ��j�b�g�͂��͈̔͂𖾎�����K�v������
        /// </summary>
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(GetAttackRangeCenter(), _attackRangeRadius);
        }
    }
}
