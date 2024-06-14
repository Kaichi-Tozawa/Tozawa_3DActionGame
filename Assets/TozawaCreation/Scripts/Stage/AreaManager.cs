using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;
[DefaultExecutionOrder(-1)]
/// <summary>
/// �J�n���ɃG���A�ɉ���z�u���邩�A
/// �y�уv���C���[�����̃G���A�ɂ��邩�E�v���C���[�ɂ���ĉ������ꂽ���ǂ�����
/// �Ǘ�����R���|�[�l���g
/// </summary>
public class AreaManager : MonoBehaviour,IPause
{
    [SerializeField, Header("��E���ɂ̐����ʒu")] Transform _objectCreatePos;
    [SerializeField, Header("�G�̐����ʒu���")] Transform[] _enemyCreatePoss;
    [SerializeField, Header("��̃v���n�u")] GameObject _tombPrefab;
    [SerializeField, Header("���ɂ̃v���n�u")] GameObject _barrackPrefab;
    [SerializeField, Header("������̕挊�v���n�u")] GameObject _locastHole;
    [SerializeField, Header("��������G�̌��z��")] GameObject[] _enemys;
    [SerializeField, Header("�������ɌĂ΂��ׂ��C�x���g�B��ɃG���A�̃Z�b�g�A�N�e�B�u�؂�ւ�")] UnityEvent _onCorrupted;
    [SerializeField, Header("�ǂꂭ�炢��������Ή����ł��邩")] float _timeForCorruption;
    float _timer;
    [SerializeField, Header("�ő�G�o����")] int _maxEnemyNum;
    int _currentStageTypeIndex = 0;
    bool _isPlayerStay = false;
    bool _isCorruptedDone = false;
    bool _isActive = true;
    StageType _currentType = StageType.None;
    private void Start()
    {
        StageSetUp();
    }

    /// <summary>
    /// �Q�[���J�n���̏���
    /// </summary>
    private void StageSetUp()
    {
        _currentStageTypeIndex = Random.Range(0, (Enum.GetValues(typeof(StageType)).Length));
        _currentType = (StageType)_currentStageTypeIndex;
        switch (_currentType)
        {
            case StageType.None: break;
            case StageType.Tomb: SetTomb(); break;
            case StageType.Barracks: SetBarracks(); break;
        }
        SetEnemy();
    }

    /// <summary>
    /// �����ɕ��z�u����
    /// </summary>
    private void SetTomb()
    {
        _tombPrefab.SetActive(true);
    }

    /// <summary>
    /// �����ɕ��ɂ�z�u����
    /// </summary>
    private void SetBarracks()
    {
        _barrackPrefab.SetActive(true);
    }

    /// <summary>
    /// �G��z�u����
    /// </summary>
    private void SetEnemy()
    {
        int rnd = Random.Range(0, _maxEnemyNum + 1);
        for (int i = 0; i < rnd ;i++) 
        {
            var enemy = Instantiate(_enemys[Random.Range(0, _enemys.Length)],
                _enemyCreatePoss[Random.Range(0,_enemyCreatePoss.Length)].position,
                Quaternion.identity);
            if(enemy.TryGetComponent<INPCComander>(out var gt))
            {
                gt.DefenceContract(_objectCreatePos.gameObject);
            }
        }
    }

    private void Update()
    {
        if(_isActive && _isPlayerStay )
        {
            CountDownForCorruption();
            if (_isCorruptedDone)
            {
                _onCorrupted.Invoke();
                _isCorruptedDone = false;
            }
        }
    }

    private void CountDownForCorruption()
    {
        _timer += Time.deltaTime;

        if(_timer >= _timeForCorruption)
        {
            _isCorruptedDone = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag.Contains("Player"))
        {
            _isPlayerStay = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            _isPlayerStay = false;
        }
    }

    void IPause.Pause()
    {
        _isActive = false;
    }

    void IPause.Reboot()
    {
        _isActive = true;
    }
}
enum StageType
{
    None = 0,
    Tomb = 1,
    Barracks = 2,
}

