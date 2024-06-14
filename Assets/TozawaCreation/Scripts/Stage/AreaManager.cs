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
/// 開始時にエリアに何を配置するか、
/// 及びプレイヤーがそのエリアにいるか・プレイヤーによって汚染されたかどうかを
/// 管理するコンポーネント
/// </summary>
public class AreaManager : MonoBehaviour,IPause
{
    [SerializeField, Header("墓・兵舎の生成位置")] Transform _objectCreatePos;
    [SerializeField, Header("敵の生成位置候補")] Transform[] _enemyCreatePoss;
    [SerializeField, Header("墓のプレハブ")] GameObject _tombPrefab;
    [SerializeField, Header("兵舎のプレハブ")] GameObject _barrackPrefab;
    [SerializeField, Header("汚染後の墓穴プレハブ")] GameObject _locastHole;
    [SerializeField, Header("生成する敵の候補配列")] GameObject[] _enemys;
    [SerializeField, Header("汚染時に呼ばれるべきイベント。主にエリアのセットアクティブ切り替え")] UnityEvent _onCorrupted;
    [SerializeField, Header("どれくらい制圧すれば汚染できるか")] float _timeForCorruption;
    float _timer;
    [SerializeField, Header("最大敵出現数")] int _maxEnemyNum;
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
    /// ゲーム開始時の処理
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
    /// 中央に墓を配置する
    /// </summary>
    private void SetTomb()
    {
        _tombPrefab.SetActive(true);
    }

    /// <summary>
    /// 中央に兵舎を配置する
    /// </summary>
    private void SetBarracks()
    {
        _barrackPrefab.SetActive(true);
    }

    /// <summary>
    /// 敵を配置する
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

