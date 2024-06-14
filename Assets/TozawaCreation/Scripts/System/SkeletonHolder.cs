using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using static SkeletonSummoner;
/// <summary>
/// プレイヤーの保持するスケルトンの保持状況を管理把握するコンポーネント
/// </summary>
public class SkeletonHolder : MonoBehaviour
{
    [SerializeField] SkeletonSummoner _skeletonSummoner;
    [SerializeField,Header("どのランクにどれくらいの数の下位スケルトンが必要なのか")]
    int[] _requiredNumbers = new int[Enum.GetValues(typeof(SkeletonRarity)).Length];
    List<GameObject> _playersSkeletons = new List<GameObject>();
    int [] _rarityCounts = new int[Enum.GetValues(typeof(SkeletonRarity)).Length];

    private void Start()
    {
        _skeletonSummoner =  _skeletonSummoner ?? GameObject.Find("SkeletonSummoner").GetComponent<SkeletonSummoner>();
    }

    /// <summary>
    /// スケルトンを召喚したときに同時に呼ばれ、進化条件を確認し、進化条件を満たしていれば一つ上のランクのスケルトンを召喚し
    /// 素材となったスケルトンを破壊する
    /// </summary>
    /// <param name="rarity">レアリティ</param>
    /// <param name="pos">生成位置</param>
    /// <param name="skeleton">召喚したスケルトン</param>
    public void  SetSkeletonInfo(SkeletonRarity rarity,Transform pos,GameObject skeleton)
    {
        if(_rarityCounts[(int)rarity] < 0)
            _rarityCounts[(int)rarity] = 0;
        _rarityCounts[(int)rarity]++;
        _playersSkeletons.Add(skeleton);
        Action action = () => { _playersSkeletons.Remove(skeleton); _rarityCounts[(int)rarity]--; };
        skeleton.AddOnDestroyCallBack(action);
        if (_rarityCounts[(int)rarity] >= _requiredNumbers[(int)rarity])
        {
            List<GameObject> material_Copy = new List<GameObject>(_playersSkeletons);
            foreach (var material in material_Copy.Where(x=>x is not null).Where(x => x.GetComponent<Rarity>().GetCurrentRarity == rarity))
            {
                Destroy(material.gameObject);
                _playersSkeletons.Remove(material);
            }
            int nextrarity = (int)rarity + 1;
            _skeletonSummoner.Summon((SkeletonRarity)nextrarity, pos);
            Debug.Log(rarity + "を" + _requiredNumbers[(int)rarity] + "体生贄に" + (SkeletonRarity)nextrarity + "召喚！");
            _rarityCounts[(int)rarity] = 0;
        }
    }
}
