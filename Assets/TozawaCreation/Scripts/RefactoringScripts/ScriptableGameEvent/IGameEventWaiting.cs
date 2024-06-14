using UnityEngine;
using UnityEngine.Events;

interface IGameEventWaiting
{
    /// <summary>
    /// イベント発生時に登録された関数を実行する
    /// </summary>
    abstract void OnEventOccur(UnityEvent @event);
}
