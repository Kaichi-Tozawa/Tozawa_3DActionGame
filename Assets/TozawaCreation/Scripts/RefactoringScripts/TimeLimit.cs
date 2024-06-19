using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
[DefaultExecutionOrder(0)]
public class TimeLimit : MonoBehaviour,IPause
{
    [SerializeField] Text _timerText;
    [SerializeField] float _timeLimit;
    [SerializeField]UnityEvent _onTimerStop = new UnityEvent();
    [SerializeField]UnityEvent _onTimerStart = new UnityEvent();
    [SerializeField, Header("時間切れになった時に行う処理")] UnityEvent _onTimeOver;
    bool _isTimerActive = false;
   
    private void Update()
    {
        if (_isTimerActive)
        {
            CountDown();
        }
    }
    void CountDown()
    {
        _timeLimit -= Time.deltaTime;
        if (_timeLimit <= 0)
        {
            _isTimerActive = false;
            _timeLimit = 0;
            _onTimeOver.Invoke();
            _timerText.text = "⚠援軍到着⚠";
        }
        else
        {
            _timerText.text = _timeLimit.ToString("F2");
        }
    }
    public void TimerSwitch(bool onoff)
    {
        _isTimerActive = onoff;
    }
    public void Pause()
    {
        _isTimerActive = false;
    }
    
    public void Reboot()
    {
        _isTimerActive = true;
        _onTimerStart.Invoke();
    }
}
