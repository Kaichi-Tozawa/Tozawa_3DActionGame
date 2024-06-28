using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventNotification : MonoBehaviour
{
    [SerializeField]GameEvent _gameEvent;
    private void Start()
    {
        _gameEvent.EventOccurrence();
    }
}
