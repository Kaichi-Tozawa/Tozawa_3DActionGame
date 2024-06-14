using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IGameEvent 
{
    /// <summary>
    /// イベントの通知契約
    /// </summary>
    public abstract void Contract(GameEventWaiting waitlist);
    /// <summary>
    /// 契約の解除
    /// </summary>
    public abstract void Termination(GameEventWaiting waitlist);
}
