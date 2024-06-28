using System.Collections;
using System.Threading;
using UnityEngine;

public class TimeCount : MonoBehaviour,IDataSource,IPause
{
    [SerializeField] GameEvent _onDataChanged;
    [SerializeField] GameEvent _onTimeOut;
    [SerializeField] float _countInterval=1;
    [SerializeField] float _maxTimeLimit;
    bool _countActive = true;
    TimeData _timeData;
    
    private void Start()
    {
        _timeData = new TimeData();
        _timeData.erapsedtime = _maxTimeLimit;
    }

    IEnumerator CountDownCorutine()
    {
        while(_countActive)
        {
            yield return new WaitForSeconds(_countInterval);
            _timeData.erapsedtime -= 1;
            if(_timeData.erapsedtime < 0)
            {
                _timeData.erapsedtime = 0;
                _countActive = false;
                StopCoroutine(CountDownCorutine());
                _onTimeOut.EventOccurrence();
            }
            OnDataChanged();
        }
    }
    TimeData IDataSource.ChangedData()
    {
        return _timeData;
    }

    public void OnDataChanged()
    {
        _onDataChanged.EventOccurrence();
    }

    void IPause.Pause()
    {
        _countActive = false;
    }

    void IPause.Reboot()
    {
        _countActive = true;
        StartCoroutine(CountDownCorutine());
    }
}
