using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 収集したアイテムの量を管理するコンポーネント
/// Playerにつける
/// </summary>
public class ItemGatheringCollectioner : MonoBehaviour
{
    [Range(0,100)]int _currentItemnum = 0;
    [SerializeField] int _requireValue;
    [SerializeField] SkeletonSummoner _summoner;
    [SerializeField] Text _showCurrentItemNumText;
    [SerializeField] Text _summonByUseItemButtonText;
    [SerializeField] GameObject _cantUsePanel;
    private void Start()
    {
        UpdateText();
    }
    /// <summary>
    /// 拾われる側から呼ばれる　所持数に+１
    /// </summary>
    public void GetItem()
    {
        _currentItemnum++;
        _currentItemnum = _currentItemnum>100 ? 100 : _currentItemnum;
        UpdateText();
    }
    /// <summary>
    /// ボタンから呼ばれる
    /// </summary>
    /// <param name="usevalue"></param>
    public void UseItem()
    {
        if(_currentItemnum >= _requireValue)
        {
            _currentItemnum = _currentItemnum - _requireValue;
            _currentItemnum = _currentItemnum <= 0 ? 0 : _currentItemnum;
            _summoner.Summon();
            UpdateText();
        }
        else
        {
            _cantUsePanel.SetActive(true); 
        }
    }
    void UpdateText()
    {
        _showCurrentItemNumText.text = "集めたソウル\r\n" + _currentItemnum.ToString();
        _summonByUseItemButtonText.text = _requireValue.ToString() + "ソウル使って召喚";
    }
}
