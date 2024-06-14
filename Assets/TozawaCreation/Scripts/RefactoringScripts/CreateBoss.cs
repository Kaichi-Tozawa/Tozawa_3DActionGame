using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBoss : MonoBehaviour
{
    [SerializeField]GameEvent gameEvent;
    private void Start()
    {
        gameEvent.EventOccurrence();
    }
}
