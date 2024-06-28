using System.Collections;
using UnityEngine;

public class CharacterAnimate :IAttackAnimate,IMoveAnimate
{
    readonly string _defaultMoveParamName;
    readonly Rigidbody _playerRB;
    Animator _playerAnim;
    readonly bool _isCheckPassed = false;
    [SerializeField]float _currentAnimSpeed = 1;
    public CharacterAnimate(Rigidbody rb,Animator anim,string moveparamname)
    {
        _playerRB = rb;
        _playerAnim = anim;
        _defaultMoveParamName = moveparamname;
        _isCheckPassed = AnimParameterSetting.CheckExist(_playerAnim, _defaultMoveParamName);
    }
    public void SetMoveVelocityParam()
    {
        if(_isCheckPassed)
        {
            AnimParameterSetting.SetParam(ref _playerAnim, _defaultMoveParamName, _playerRB.velocity.magnitude);
        }
    }
    public void SetAttackBool(string paramname,bool onoff)
    {
        AnimParameterSetting.SetParam(ref _playerAnim, paramname,onoff);
    }

    void IPause.Pause()
    {
        _playerAnim.speed = 0;
    }

    void IPause.Reboot()
    {
        _playerAnim.speed = _currentAnimSpeed;
    }
}
