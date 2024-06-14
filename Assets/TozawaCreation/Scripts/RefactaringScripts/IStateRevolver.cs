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
    ///// <summary>
    ///// 時間に応じてステートを進めたり渡されたアクションを実行するコルーチン
    ///// </summary>
    ///// <param name="action">なんかさせたい処理</param>
    ///// <param name="operatingtime">そのステートの稼働時間</param>
    ///// <returns></returns>
    //abstract IEnumerator StateBehaviorTimerCorutine(Action action,float operatingtime);
}
