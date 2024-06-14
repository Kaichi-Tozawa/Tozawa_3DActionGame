using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IGameEvent 
{
    /// <summary>
    /// �C�x���g�̒ʒm�_��
    /// </summary>
    public abstract void Contract(GameEventWaiting waitlist);
    /// <summary>
    /// �_��̉���
    /// </summary>
    public abstract void Termination(GameEventWaiting waitlist);
}
