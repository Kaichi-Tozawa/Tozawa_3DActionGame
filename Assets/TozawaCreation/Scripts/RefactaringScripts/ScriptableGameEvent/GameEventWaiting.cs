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
    /// イベント発生（実動作）
    /// </summary>
    /// <param name="event">GameEvent側に定義されたUnityEvent</param>
    public void OnEventOccur(UnityEvent @event)
    {
        @event.Invoke();
    }
    /// <summary>
    /// 通知待ち側のUnityEventを実行
    /// </summary>
    public void OnEventOccur()
    {
        _notificateEvents.Invoke();
    }
}