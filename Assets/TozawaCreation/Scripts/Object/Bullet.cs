using Attack;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bullet : MonoBehaviour,IPause
{
    GameObject _targetObject;
    Rigidbody _rb;
    float _bulletSpeed;
    float _impact;
    int _damage;
    bool _isActive = true;
    string _targetTag;
    BulletType _currentType;
    Vector3 _temporaryVerocity = Vector3.zero;

    public void Reload(BulletInfo info)
    {
        _damage = info.damage;
        _impact = info.impactpow;
        _bulletSpeed = info.bulletspeed;
        _targetTag = info.enemytag;
        _currentType = info.bulletType;
    }
    private void Start()
    {
        _targetTag = this.tag == "PlayerSide" ? "EnemySIde" : "PlayerSide";
        _rb = GetComponent<Rigidbody>();
        if(_currentType == BulletType.Homing )
        {
            _targetObject = GetTarget();
            Debug.Log(_targetObject.name);
        }
    }

    GameObject GetTarget()
    {
        try
        {
            GameObject mostNearObject = GameObject.FindGameObjectsWithTag(_targetTag)
                .Where(x => x.GetComponent<Health>())
                .Aggregate((current, next) =>
                Vector3.Distance(current.transform.position, transform.position)
                < Vector3.Distance(next.transform.position, transform.position) ? current : next);
            return mostNearObject;
        }
        catch (System.InvalidOperationException)
        {
            return this.gameObject;
        }
    }
    private void FixedUpdate()
    {
        if(_isActive)
        {
            BulletMove();
            _rb.velocity = this.transform.forward * _bulletSpeed;
        }
    }
    void BulletMove()
    {
        if (_currentType == BulletType.Straight)
            return;
        if(_targetObject==this.gameObject || _targetObject == null)
            return;
        var dir = _targetObject.transform.position;
        dir.y = 0;
        transform.LookAt(dir);
        Debug.Log("ホーミング中");
    }

    private void OnCollisionEnter(Collision collision)
    {

        BulletHit(collision);
        Destroy(this.gameObject);
    }
    void BulletHit(Collision collision)
    {
        if ( ! collision.gameObject.CompareTag(_targetTag))
            return;
        if (collision.gameObject.TryGetComponent<IHealth>(out var health))
        {
            health.TakeDamage(_damage);
        }
        if (collision.gameObject.TryGetComponent<Rigidbody>(out var rv))
        {
            Vector3 dir = Vector3.forward;
            _rb.AddForce(dir * _impact, ForceMode.Impulse);
        }
    }

    public void Pause()
    {
        _isActive = false;
        _temporaryVerocity = _rb.velocity;
        _rb.velocity = Vector3.zero;
        this.gameObject.GetComponent<EffectLifeTime>().Pause();
    }

    public void Reboot()
    {
        _isActive = true;
        _rb.velocity = _temporaryVerocity;
        this.gameObject.GetComponent<EffectLifeTime>().Reboot();
    }
}