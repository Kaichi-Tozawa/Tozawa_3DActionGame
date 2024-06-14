using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventOccurTest : MonoBehaviour
{
    [SerializeField] GameEvent _targetEvent;
    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            _targetEvent.EventOccurrence();
        }
    }
    public void TestLog()
    {
        Debug.Log("ŒÄ‚Ño‚µ¬Œ÷");
    }
}
