using GameSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 体力を管理するコンポーネント
/// </summary>
public class Health : MonoBehaviour,IHealth
{
    [SerializeField,Header("体力の最大値")] int _initialHealth =100;
    [SerializeField,Header("ドロップアイテム")] GameObject _dropItemPrefab;
    [SerializeField, Header("死亡時のエフェクト")] GameObject _deathEffect;
    [SerializeField, Header("ラグドールの場合は腰のボーン位置を指定")] Transform _ragDollRootBone;
    [SerializeField, Header("オブジェクト生成位置。いい感じに調整してください")] Transform _createPoint;
    int _currentHealth = 100;
    private void Awake()
    {
        _currentHealth = _initialHealth;
    }
    void IHealth.TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Death();
        }
        else if (_currentHealth >= _initialHealth)
        {
            _currentHealth = _initialHealth;
        }
    }

    int IHealth.CurrentHealth()
    {
        return _currentHealth;
    }

    void Death()
    {
        if (_dropItemPrefab)
        {
            Instantiate(_dropItemPrefab, _createPoint.position, transform.rotation);
        }
        if (_deathEffect)
        {
            Instantiate(_deathEffect, _createPoint.position, transform.rotation);
        }
        else if (_deathEffect && _ragDollRootBone)
        {
            SpawnRagDoll(_deathEffect, _ragDollRootBone);
        }
        Destroy(this.gameObject);
    }
    void SpawnRagDoll(GameObject ragdoll, Transform rootpos)
    {
        var doll = Instantiate(ragdoll, rootpos.transform.position, transform.rotation);
        RagDollSetup ragDollSetup = doll.GetComponent<RagDollSetup>();
        ragDollSetup.SetUp(rootpos);
        doll.AddComponent<Rigidbody>().AddForce(transform.forward ,ForceMode.Impulse);
    }
}