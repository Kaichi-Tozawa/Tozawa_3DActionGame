using UnityEngine;
using UnityEngine.Events;

interface IGameEventWaiting
{
    /// <summary>
    /// �C�x���g�������ɓo�^���ꂽ�֐������s����
    /// </summary>
    abstract void OnEventOccur(UnityEvent @event);
}
