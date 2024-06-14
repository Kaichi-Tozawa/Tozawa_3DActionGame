using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ���W�����A�C�e���̗ʂ��Ǘ�����R���|�[�l���g
/// Player�ɂ���
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
    /// �E���鑤����Ă΂��@��������+�P
    /// </summary>
    public void GetItem()
    {
        _currentItemnum++;
        _currentItemnum = _currentItemnum>100 ? 100 : _currentItemnum;
        UpdateText();
    }
    /// <summary>
    /// �{�^������Ă΂��
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
        _showCurrentItemNumText.text = "�W�߂��\�E��\r\n" + _currentItemnum.ToString();
        _summonByUseItemButtonText.text = _requireValue.ToString() + "�\�E���g���ď���";
    }
}
