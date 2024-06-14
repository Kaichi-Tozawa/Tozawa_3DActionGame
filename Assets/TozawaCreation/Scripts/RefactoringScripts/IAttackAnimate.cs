using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 攻撃アニメーションの挙動を定義するインターフェース
/// </summary>
 interface IAttackAnimate
{
    /// <summary>
    /// 攻撃アニメーションステートに遷移するフラグ
    /// </summary>
    /// <param name="paramname">攻撃パラメーターの名前</param>
    /// <param name="onoff">ブール値</param>
    abstract void SetAttackBool(string paramname, bool onoff);
}
