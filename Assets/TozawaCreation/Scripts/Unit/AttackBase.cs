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
        [SerializeField, Header("ダメージとなる攻撃力")] int _attackPower = 0;
        [SerializeField, Header("ノックバックさせる衝撃力")] float _impactPower;
        [SerializeField, Header("攻撃がヒットしたときのエフェクト")] GameObject _hitEffect;
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
