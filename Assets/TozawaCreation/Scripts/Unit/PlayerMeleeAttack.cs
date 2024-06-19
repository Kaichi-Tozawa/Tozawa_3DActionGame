using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace Attack
{
    /// <summary>
    /// プレイヤーが行う近接攻撃を管理するクラス
    /// </summary>
    public class PlayerMeleeAttack : MeleeAttack
    {
        [SerializeField, Header("ヒット時にアニメーション速度が変化する効果時間")] float _delayTimeforHitStop;
        [SerializeField, Header("ヒット時に適用させるアニメーション速度（０でヒットストップ）")] float _delayTimeScale = 0;
        IHealth _targetIH; 
        Animator _playerAnimator;
        bool _isHitStop= false;
        private void Start()
        {
            _playerAnimator = GetComponent<Animator>();
        }
        /// <summary>
        /// プレイヤーの攻撃だけはヒットストップさせる
        /// </summary>
        private protected override void AttackEvent()
        {
            foreach (var target in Physics.OverlapSphere(base.GetAttackRangeCenter(), base.AttackRangeRadius())
                .Where(x => !x.gameObject.CompareTag(base.OwnTag()) && x.TryGetComponent<IHealth>(out _targetIH))) 
            {
                if (!_isHitStop)
                {
                    _isHitStop = true;
                    StartCoroutine(HitStopCoroutine(() => { _isHitStop = false; }));
                    Instantiate(base.HitEffect(), GetAttackRangeCenter(), this.transform.rotation);
                }
                var targetRB = target.GetComponent<Rigidbody>();
                if (targetRB != null)
                {
                    targetRB.AddForce(this.transform.forward * base.ImpactPower(), ForceMode.Impulse);
                }
                _targetIH.TakeDamage(base.DamagePower());//ダメージを与える
            }
        }

        /// <summary>
        /// ヒットストップを実行するコルーチン
        /// </summary>
        /// <param name="callback">完了時にさせたい処理</param>
        /// <returns></returns>
        IEnumerator HitStopCoroutine(System.Action callback)
        {
            var instantAnimSpeed = _playerAnimator.speed;
            _playerAnimator.speed = _delayTimeScale;
            yield return new WaitForSeconds(_delayTimeforHitStop);
            _playerAnimator.speed = instantAnimSpeed;
            callback();
        } 
    }
}
