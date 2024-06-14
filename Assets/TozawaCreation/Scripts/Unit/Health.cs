using GameSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �̗͂��Ǘ�����R���|�[�l���g
/// </summary>
public class Health : MonoBehaviour,IHealth
{
    [SerializeField,Header("�̗͂̍ő�l")] int _initialHealth =100;
    [SerializeField,Header("�h���b�v�A�C�e��")] GameObject _dropItemPrefab;
    [SerializeField, Header("���S���̃G�t�F�N�g")] GameObject _deathEffect;
    [SerializeField, Header("���O�h�[���̏ꍇ�͍��̃{�[���ʒu���w��")] Transform _ragDollRootBone;
    [SerializeField, Header("�I�u�W�F�N�g�����ʒu�B���������ɒ������Ă�������")] Transform _createPoint;
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