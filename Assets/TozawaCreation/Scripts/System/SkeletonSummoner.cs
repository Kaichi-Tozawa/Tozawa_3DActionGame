using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Linq;
using System;
/// <summary>
/// スケルトンを生成し,命令UIを表示し,UIから受け取った命令をスケルトンに打ち込む
/// </summary>
public class SkeletonSummoner : MonoBehaviour
{
    [SerializeField, Header("全スケルトンを弱い順に登録してください")]
    GameObject[] _skeletons = new GameObject[Enum.GetValues(typeof(SkeletonRarity)).Length];
    [SerializeField, Header("召喚時エフェクト")] GameObject _summonEffect;
    [SerializeField, Header("Player")] GameObject _player;
    bool _isCommanded = false;
    CommandBase _commandBase;
    Transform _playerPos;

    /// <summary>
    /// 召喚時に実行すべきこと
    /// </summary>
    [SerializeField, Header("召喚時に実行すべき処理\r\n①StopEventHandlerにオブジェクトを止めさせる\r\n    /// ②コマンドUIの表示")] 
    UnityEvent _onSummon;

    /// <summary>
    /// 召喚完了時に実行すべきこと
    /// </summary>
    [SerializeField, Header("召喚完了時に実行すべき処理\r\n①StopEventHandlerにオブジェクトの動きを再始動させる")]
    UnityEvent _onFinished;

    /// <summary>
    /// UI関連
    /// </summary>
    [SerializeField,Header("どのレアリティに命令するかを示すUI")] Text _showRarityText;
    private void Start()
    {
        _isCommanded = false;
        _playerPos = _player.transform;
    }

    public void Summon(SkeletonRarity rarity, Transform pos)
    {
        _isCommanded = false;
        StartCoroutine(SummonSkeleton(rarity, pos));
    }

    public void Summon()
    {
        _isCommanded = false;
        StartCoroutine(SummonSkeleton(SkeletonRarity.zako,_playerPos));
    }

    /// <summary>
    /// スケルトンを生成し命令をセットする
    /// </summary>
    /// <param name="rarity">生成するスケルトンのランク</param>
    /// <param name="transform">生成する場所</param>
    IEnumerator SummonSkeleton(SkeletonRarity rarity,Transform pos)
    {
        var skeleton = Instantiate(_skeletons[(int)rarity], pos.position, Quaternion.identity);
        _onSummon.Invoke();
        SetRarityOnUICommand(rarity);
        yield return new  WaitUntil(() => _isCommanded);
        Instantiate(_summonEffect, skeleton.transform.position, Quaternion.identity);
        _commandBase.OnSetCommand(skeleton);
        _onFinished.Invoke();
    }
    /// <summary>
    /// コマンドボタンから呼ばれる
    /// </summary>
    public void SetCommanded(CommandBase command)
    {
        _commandBase = command;
        _isCommanded = true;
    }

    public void SetRarityOnUICommand(SkeletonRarity rarity)
    {
        string skeletonName = "素手スケルトン";
        if(rarity == SkeletonRarity.zako)
        {
            skeletonName = "素手スケルトン";
        }
        else if(rarity == SkeletonRarity.sword)
        {
            skeletonName = "剣士スケルトン";
        }
        else if (rarity == SkeletonRarity.magic)
        {
            skeletonName = "魔導士スケルトン";
        }
        else if (rarity == SkeletonRarity.giant)
        {
            skeletonName = "巨人スケルトン";
        }
        _showRarityText.text = skeletonName + "に何を命令しますか？";
    }
}
