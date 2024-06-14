using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bullet : MonoBehaviour,IPauseAble
{
    float _bulletSpeed;
    BulletType _currentType;
    string _targetTag;
    float _impact;
    int _damage;
    GameObject _hitEffect;
    GameObject _targetObject;
    Rigidbody _rb;
    string _ownTag;

    bool _isActive = true;

    public int Damage
    { set { _damage = value; } }
    public float Impact
    { set { _impact = value; } }
    public float BulletSpeed
    {  set { _bulletSpeed = value; } }
    public GameObject HitEffect
    { set { _hitEffect = value; } }
    public string OwnTag
    { set { _ownTag = value; } }
    public BulletType CurrentType
    { set { _currentType = value; } }
    const string PLAYER_TAG = "PlayerSide";
    const string ENEMY_TAG = "EnemySide";
    Vector3 _temporaryVerocity = Vector3.zero;
    private void Start()
    {
        _targetTag = (_ownTag == PLAYER_TAG) ? ENEMY_TAG : PLAYER_TAG;
        _rb = GetComponent<Rigidbody>();
        if(_currentType == BulletType.Homing )
        {
            GetTarget();
            
        }
    }

    void GetTarget()
    {
        try
        {
            GameObject mostNearObject = GameObject.FindGameObjectsWithTag(_targetTag).Where(x => x.GetComponent<NPCComander>())
                .Aggregate((current, next) =>
                Vector3.Distance(current.transform.position, transform.position)
                < Vector3.Distance(next.transform.position, transform.position) ? current : next);
            _targetObject = mostNearObject;
        }
        catch (System.InvalidOperationException)
        {
            _targetObject = this.gameObject;
        }
    }
    private void FixedUpdate()
    {
        if(_isActive)
        {
            switch (_currentType)
            {
                case BulletType.Straight:
                    GoStraight();
                    break;
                case BulletType.Homing:
                    Homing();
                    break;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(_targetTag))
        {
            if (_hitEffect)
            {
                Instantiate(_hitEffect, collision.transform.position, this.transform.rotation);
            }
            if (collision.gameObject.TryGetComponent<IHealth>(out var health))
            {
                health.TakeDamage(_damage);
            }
            if (collision.gameObject.TryGetComponent<Rigidbody>(out var rv))
            {
                Vector3 dir = Vector3.forward;
                _rb.AddForce(dir * _impact, ForceMode.Impulse);
            }
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// ”­ŽËˆÊ’u‚©‚ç
    /// </summary>
    void GoStraight()
    {
        _rb.velocity = this.transform.forward * _bulletSpeed;
    }
    void Homing()
    {
        if(_targetObject == this.gameObject)
        {
            GoStraight();
        }
        else if (_targetObject)
        {
            transform.LookAt(_targetObject.transform.position);
            _rb.velocity = this.transform.forward * _bulletSpeed;
        }
        else
        {
            GoStraight();
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
