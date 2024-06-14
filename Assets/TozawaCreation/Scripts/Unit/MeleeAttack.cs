using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Attack
{
    /// <summary>
    /// 近接攻撃
    /// </summary>
    public class MeleeAttack : AttackBase
    {
        [SerializeField, Header("攻撃範囲球体の中心点")] Vector3 _attackRangeCenter;
        [SerializeField, Header("攻撃範囲球体の半径")] float _attackRangeRadius;
        IHealth _targetIH;
        protected float AttackRangeRadius
        {
            get { return _attackRangeRadius; }
        }
        /// <summary>
        /// 全ての近接攻撃はPhysics.Overlapで攻撃処理を行う。これはアニメーションイベントから呼ばれる関数である
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
        /// 攻撃範囲の中心点を取る
        /// </summary>
        /// <returns>攻撃範囲の中心点(Vector3)</returns>
        protected Vector3 GetAttackRangeCenter()
        {
            Vector3 center = this.transform.position + this.transform.forward * _attackRangeCenter.z
                + this.transform.up * _attackRangeCenter.y
                + this.transform.right * _attackRangeCenter.x;
            return center;
        }

        /// <summary>
        /// 攻撃範囲を赤い線でシーンビューに表示する。近接攻撃を持つ全てのユニットはその範囲を明示する必要がある
        /// </summary>
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(GetAttackRangeCenter(), _attackRangeRadius);
        }
    }
}
