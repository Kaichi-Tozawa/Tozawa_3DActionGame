using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Attack
{
    /// <summary>
    /// 遠距離攻撃を持つNPCの遠距離攻撃を管理するコンポーネント
    /// </summary>
    public class RangedAttack : AttackBase
    {
        [SerializeField, Header("発射するゲームオブジェクトのプレハブ")] GameObject _bullet;
        [SerializeField, Header("発射する位置")] Transform _nozzle;
        [SerializeField, Header("弾のタイプ")] BulletType bulletType;
        [SerializeField, Header("弾の移動スピード")] float _bulletSpeed;
        /// <summary>
        /// 発射位置に弾を生成するコンポーネント
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
