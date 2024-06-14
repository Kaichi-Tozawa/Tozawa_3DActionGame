using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
/// <summary>
/// �ǂɂԂ������Ƃ��ɐ^���ɉI�񂵂Đi�ރR���|�[�l���g
/// </summary>
public class WallAvoid : MonoBehaviour,IWallAvoid
{
    [SerializeField, Header("�ǂꂭ�炢�̃X�s�[�h�ŉI�񂷂邩")] float _moveSpeed=5;
    [SerializeField, Header("�ǂꂭ�炢�̎��ԉI������݂邩")] float _timeToAvoid=3;

    Rigidbody _rb;
    INPCComander _iNPCC;
    const int LeftAngle = -90;
    bool _isAvoidActive = false;
    bool _isWalled =false;
    bool _isCompornentActivate = false;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _iNPCC = GetComponent<NPCComander>();

    }
    private void FixedUpdate()
    {
        if(_isAvoidActive&& _isCompornentActivate)
        {
            _rb.velocity = this.transform.forward * _moveSpeed;
        }
    }
    IEnumerator MoveAvoidRoot()
    {
        _isAvoidActive = true;
        this.transform.eulerAngles += new Vector3(0, LeftAngle, 0);
        yield return new WaitForSeconds(_timeToAvoid);
        _isAvoidActive = false;
        if(_isWalled)
        {
            StartCoroutine(MoveAvoidRoot());
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            StartCoroutine(MoveAvoidRoot());
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            _isWalled = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            _isWalled = false;
        }
    }

    void IWallAvoid.Pause()
    {
        _isCompornentActivate = true;
        StopAllCoroutines();
    }

    void IWallAvoid.Reboot()
    {
        _isCompornentActivate = false;
    }
}
