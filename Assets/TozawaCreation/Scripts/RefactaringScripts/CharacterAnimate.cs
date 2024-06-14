using System.Collections;
using UnityEngine;

public class CharacterAnimate :IAttackAnimate,IMoveAnimate
{
    string _defaultMoveParamName; 
    Rigidbody _playerRB;
    Animator _playerAnim;
    bool _isCheckPassed = false;
    [SerializeField]float _currentAnimSpeed = 1;
    public CharacterAnimate(Rigidbody rb,Animator anim,string moveparamname)
    {
        _playerRB = rb;
        _playerAnim = anim;
        _defaultMoveParamName = moveparamname;
        _isCheckPassed = AnimParamerterDividing.CheckExist(_playerAnim, _defaultMoveParamName);
    }


    public void SetMoveVelocityParam()
    {
        if(_isCheckPassed)
        {
            AnimParamerterDividing.SetParam(ref _playerAnim, _defaultMoveParamName, _playerRB.velocity.magnitude);
        }
    }


    public void SetAttackBool(string paramname,bool onoff)
    {
        AnimParamerterDividing.SetParam(ref _playerAnim, paramname,onoff);
    }

    void IPauseAble.Pause()
    {
        
        _playerAnim.speed = 0;
    }

    void IPauseAble.Reboot()
    {
        _playerAnim.speed = _currentAnimSpeed;
    }
}
