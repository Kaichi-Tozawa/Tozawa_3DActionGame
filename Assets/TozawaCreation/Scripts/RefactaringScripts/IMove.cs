using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IMove:IPauseAble
{
    public float MoveSpeed();
    public void Move(Vector3 dir);
    public abstract void  Behold(Vector3 dir);
    abstract void Brake();
}
