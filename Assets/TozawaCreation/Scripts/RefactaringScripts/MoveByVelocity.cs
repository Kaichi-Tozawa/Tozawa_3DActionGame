using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByVelocity : IMove
{
    Rigidbody _rb;
    float _moveSpeed;
    Vector3 _currentVelocity;
    public MoveByVelocity(Rigidbody rb, float speed = 5f)
    {
        _moveSpeed = speed;
        _rb = rb;
    }

    

    void IMove.Brake()
    {
        _rb.velocity = Vector3.zero;
    }

    void IMove.Move(Vector3 dir)
    {
        var direction = _moveSpeed * dir.normalized;
        direction.y = _rb.velocity.y;
        _rb.velocity = direction;
        if(dir!=Vector3.zero)
        {
            Behold(dir);
        }
    }
    public void Behold(Vector3 dir)
    {
        if(dir != Vector3.zero)
        {
            dir.y = _rb.velocity.y;
            _rb.rotation = Quaternion.LookRotation(dir);
        }
    }

    float IMove.MoveSpeed()
    {
        return _moveSpeed;
    }

    void IPauseAble.Pause()
    {
        _currentVelocity = _rb.velocity;
        _rb.velocity = Vector3.zero;
    }

    void IPauseAble.Reboot()
    {
        _rb.velocity = _currentVelocity;
    }
}
