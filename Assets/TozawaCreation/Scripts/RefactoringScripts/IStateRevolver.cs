using System;
using System.Collections;

interface IStateRevolver<T>where T : Enum
{
    /// <summary>
    /// 現在のステートを返す
    /// </summary>
    abstract T CurrentState();
    /// <summary>
    /// 次のステートへ
    /// </summary>
    abstract void NextState();
}
