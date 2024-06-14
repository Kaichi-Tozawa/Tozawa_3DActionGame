using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccompanyCommand : CommandBase
{
    [SerializeField] SkeletonHolder _skeletonHolder;
    protected override void SetCommand(GameObject skeleton)
    {
        var rarity = skeleton.GetComponent<Rarity>().GetCurrentRarity;
        _skeletonHolder.SetSkeletonInfo(rarity,skeleton.transform,skeleton);
        var inpcc = skeleton.GetComponent<INPCComander>();
        inpcc.DefenceContract(GameObject.Find("Player"));
    }
}
