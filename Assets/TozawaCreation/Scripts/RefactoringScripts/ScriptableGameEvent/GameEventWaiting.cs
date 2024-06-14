using UnityEngine;
using UnityEngine.Events;

public class GameEventWaiting : MonoBehaviour
{
    [SerializeField] GameEvent[] _gameEvents;
    [SerializeField] UnityEvent _notificateEvents;
    private void OnEnable()
    {
        foreach (GameEvent gameEvent in _gameEvents)
        {
            gameEvent.Contract(this);
        }
    }
    private void OnDisable()
    {
        foreach(GameEvent gameEvent in _gameEvents)
        {
            gameEvent.Termination(this);
        }
    }
    /// <summary>
    /// �C�x���g�����i������j
    /// </summary>
    /// <param name="event">GameEvent���ɒ�`���ꂽUnityEvent</param>
    public void OnEventOccur(UnityEvent @event)
    {
        @event.Invoke();
    }
    /// <summary>
    /// �ʒm�҂�����UnityEvent�����s
    /// </summary>
    public void OnEventOccur()
    {
        _notificateEvents.Invoke();
    }
}