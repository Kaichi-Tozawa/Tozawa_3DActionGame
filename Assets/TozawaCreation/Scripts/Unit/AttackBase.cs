using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Attack
{
    /// <summary>
    /// あらゆる攻撃の基礎となる親クラス
    /// </summary>
    public class AttackBase : MonoBehaviour
    {
        [SerializeField, Header("ダメージとなる攻撃力")] int _damage = 0;
        [SerializeField, Header("ノックバックさせる衝撃力")] float _impact;
        [SerializeField, Header("攻撃がヒットしたときのエフェクト")] GameObject _hitEffect;
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
