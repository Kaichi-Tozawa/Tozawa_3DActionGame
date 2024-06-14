using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DefaultExecutionOrder(1)]
public class PlayerInput : MonoBehaviour,IPause
{
    [SerializeField, Header("移動速度")] float _moveSpeed;
    [SerializeField,Header("アニメーション移動パラメーターの名前")] string _defaultMoveParamName;
    [SerializeField, Header("攻撃用パラメーターの名前")] string _attackParamName;
    [SerializeField] InterfaceMediary<IVirtualStick> _Imediater;
    
    Rigidbody _rb;
    Animator _animator;
    IAttackAnimate _attackAnimate;
    IMoveAnimate _moveAnimate;
    IMove _move;
    IVirtualStick _vStick;
    Vector3 _moveDirection = Vector3.zero;
    bool _appFinished = false;
    bool _attackParambool = false;
    bool _isCorutineRunning = false;
    bool _isPause = false;
    private void Awake()
    {
        InitialSetting();
    }
    private void Update()
    {
        if (_isPause){ return; }
        _moveAnimate.SetMoveVelocityParam();
        _attackAnimate.SetAttackBool(_attackParamName, _attackParambool);
        InjectInput();
        ObservAttack();
    }

    private void FixedUpdate()
    {
        if (_isPause) { return; }
        Move();
    }

    private void ObservAttack()
    {
        if (_vStick.Pressed())
        {
            StartCoroutine(DetectAttack());
        }
    }

    private void InjectInput()
    {
        _moveDirection.x = _vStick.HorizonInput();
        _moveDirection.z = _vStick.VerticalInput();
    }

    private void Move()
    {
        var dir = (_vStick.Pressed() && !_attackParambool) ? _moveDirection : Vector3.zero;
        _move.Move(dir);
    }
    IEnumerator  DetectAttack()
    {
        if(_isCorutineRunning)yield break; _isCorutineRunning = true;
        yield return new WaitWhile(() => _vStick.Pressed());
        _attackParambool = true;
        _isCorutineRunning = false;
    }
    public void AttackFinished()
    {
        _attackParambool = false;
    }
    public void TestMove()
    {
        Move();
    }
    private void InitialSetting()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        var movebyvelocity = new MoveByVelocity(_rb, _moveSpeed);
        var characterAnimate = new CharacterAnimate(_rb, _animator, _defaultMoveParamName);
        _move = movebyvelocity;
        _attackAnimate = characterAnimate;
        _moveAnimate = characterAnimate;
        _vStick = _Imediater.Interface();
        if(_vStick==null)
        {
            Debug.LogError("ヴァーチャルスティックをアタッチしてください");
        }
    }
    private void OnApplicationQuit()
    {
        _appFinished = true;
    }

    private void OnDestroy()
    {
        if (!_appFinished)
        {
            _vStick.DisAble();
        }
    }

    public void Pause()
    {
        _move.Pause();
        _moveAnimate.Pause();
        _isPause = true;
    }

    public void Reboot()
    {
        _move.Reboot();
        _moveAnimate.Reboot();
        _isPause = false;
    }
}
