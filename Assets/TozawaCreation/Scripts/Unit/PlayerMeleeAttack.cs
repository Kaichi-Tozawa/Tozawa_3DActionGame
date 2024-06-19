using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace Attack
{
    /// <summary>
    /// �v���C���[���s���ߐڍU�����Ǘ�����N���X
    /// </summary>
    public class PlayerMeleeAttack : MeleeAttack
    {
        [SerializeField, Header("�q�b�g���ɃA�j���[�V�������x���ω�������ʎ���")] float _delayTimeforHitStop;
        [SerializeField, Header("�q�b�g���ɓK�p������A�j���[�V�������x�i�O�Ńq�b�g�X�g�b�v�j")] float _delayTimeScale = 0;
        IHealth _targetIH; 
        Animator _playerAnimator;
        bool _isHitStop= false;
        private void Start()
        {
            _playerAnimator = GetComponent<Animator>();
        }
        /// <summary>
        /// �v���C���[�̍U�������̓q�b�g�X�g�b�v������
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
                _targetIH.TakeDamage(base.DamagePower());//�_���[�W��^����
            }
        }

        /// <summary>
        /// �q�b�g�X�g�b�v�����s����R���[�`��
        /// </summary>
        /// <param name="callback">�������ɂ�����������</param>
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
