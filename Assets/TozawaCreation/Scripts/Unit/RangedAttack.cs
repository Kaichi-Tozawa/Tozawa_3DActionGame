using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace Attack
{
    /// <summary>
    /// £UðÂNPCÌ£UðÇ·éR|[lg
    /// </summary>
    public class RangedAttack : AttackBase
    {
        [SerializeField, Header("­Ë·éQ[IuWFNgÌvnu")] GameObject _bullet;
        [SerializeField, Header("­Ë·éÊu")] Transform _nozzle;
        [SerializeField, Header("eÌ^Cv")] BulletType _bulletType;
        [SerializeField, Header("eÌÚ®Xs[h")] float _bulletSpeed;
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
