using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimeLimitUI : MonoBehaviour,IDataDestination
{
    TimeData _timedata;
    [SerializeField] Text _text;
    [SerializeField] String _onTimeOutText = "“G‚Ì‰‡ŒR‚ª“ž’…";
    private void Start()
    {
        _text = GetComponent<Text>();
    }
    void IDataDestination.ReceiveData(TimeData timedata)
    {
        _timedata = timedata;
    }

    public void UpdateData()
    {
        _text.text = _timedata.erapsedtime == 0 ?_onTimeOutText :_timedata.erapsedtime.ToString();
    }
    
}
