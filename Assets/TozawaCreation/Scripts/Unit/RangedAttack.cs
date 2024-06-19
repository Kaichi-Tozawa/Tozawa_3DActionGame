using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace Attack
{
    /// <summary>
    /// 遠距離攻撃を持つNPCの遠距離攻撃を管理するコンポーネント
    /// </summary>
    public class RangedAttack : AttackBase
    {
        [SerializeField, Header("発射するゲームオブジェクトのプレハブ")] GameObject _bullet;
        [SerializeField, Header("発射する位置")] Transform _nozzle;
        [SerializeField, Header("弾のタイプ")] BulletType _bulletType;
        [SerializeField, Header("弾の移動スピード")] float _bulletSpeed;
        BulletInfo _bulletInfo;
        private void Start()
        {
            _bulletInfo = new BulletInfo();
            _bulletInfo.damage = base.DamagePower();
            _bulletInfo.impactpow = base.ImpactPower();
            _bulletInfo.bulletspeed = _bulletSpeed;
            _bulletInfo.bulletType = _bulletType;
            _bulletInfo.enemytag = this.tag == "PlayerSide" ? "EnemySide" : "PlayerSide";
        }
        private protected override void AttackEvent()
        {
            var obj =  Instantiate(_bullet, _nozzle.position, this.transform.rotation);
            obj.GetComponent<Bullet>().Reload(_bulletInfo);
            Action callback = () =>
            {
                Instantiate(base.HitEffect(), obj.transform.position, Quaternion.identity);
            };
            obj.AddOnDestroyCallBack(callback);
            
        }
    }
    public struct BulletInfo
    {
        public int damage;
        public float impactpow;
        public float bulletspeed;
        public string enemytag;
        public BulletType bulletType;
    }
}
