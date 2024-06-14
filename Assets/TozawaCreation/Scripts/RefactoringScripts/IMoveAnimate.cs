using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 移動アニメーションの挙動を定義するインターフェース
/// </summary>
interface IMoveAnimate :IPause 
{
    abstract void SetMoveVelocityParam();
}
