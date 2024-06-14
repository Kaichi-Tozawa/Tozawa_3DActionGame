using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[DefaultExecutionOrder(0)]
public class IntroductionGuideUI : MonoBehaviour
{
    [SerializeField] GameEvent _pauseEvent;
    [SerializeField] GameEvent _restartEvent;
    [SerializeField] IntroductionGuideData[] _guideDataList;
    [SerializeField] Image _guideImage;
    [SerializeField] Text _guideText;
    IntroductionGuideData _currentData;
    int _currentIndex = 0;
    private void OnEnable()
    {
        _pauseEvent.EventOccurrence();
    }
    private void Start()
    {
        ReferData();
        Reflection();
        _pauseEvent.EventOccurrence();
    }
    void ReferData()
    {
        var length = _guideDataList.Length;
        _currentData = _guideDataList[_currentIndex % length];
    }
    void Reflection()
    {
        _guideImage.sprite = _currentData.GuideImage();
        _guideText.text = _currentData.GuideText();
    }
    
    public void Revolbing() 
    {
        _currentIndex ++;
        ReferData();
        Reflection();
    }
    public void FinishGuide()
    {
        _restartEvent.EventOccurrence();
        Destroy(this.gameObject);
    }
    
}
