using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Attack
{
    /// <summary>
    /// �������U��������NPC�̉������U�����Ǘ�����R���|�[�l���g
    /// </summary>
    public class RangedAttack : AttackBase
    {
        [SerializeField, Header("���˂���Q�[���I�u�W�F�N�g�̃v���n�u")] GameObject _bullet;
        [SerializeField, Header("���˂���ʒu")] Transform _nozzle;
        [SerializeField, Header("�e�̃^�C�v")] BulletType bulletType;
        [SerializeField, Header("�e�̈ړ��X�s�[�h")] float _bulletSpeed;
        /// <summary>
        /// ���ˈʒu�ɒe�𐶐�����R���|�[�l���g
        /// </summary>
        public void AttackEvent()
        {
            var obj =  Instantiate(_bullet, _nozzle.position, this.transform.rotation);
            var bullet = obj.GetComponent<Bullet>();
            bullet.Damage = (int)base.AttackPower;
            bullet.Impact = base.ImpactPower;
            bullet.HitEffect = base.HitEffect;
            bullet.OwnTag = base.OwnTag;
            bullet.CurrentType = bulletType;
            bullet.BulletSpeed =_bulletSpeed;
        }
    }
}
