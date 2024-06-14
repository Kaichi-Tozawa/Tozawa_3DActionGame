using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificeCommand : CommandBase
{
    [SerializeField] InterfaceMediary<IHealth> _interface;
    [SerializeField, Header("生贄時エフェクト")] GameObject _sacrificeEffect;
    [SerializeField, Header("スケルトンを消費した際の回復量を弱い順に登録してください")]
    int[] _healValues = new int[Enum.GetValues(typeof(SkeletonRarity)).Length];
    protected override void SetCommand(GameObject skeleton)
    {
        Instantiate(_sacrificeEffect, skeleton.transform.position, Quaternion.identity);
        int rarity = (int)skeleton.GetComponent<Rarity>().GetCurrentRarity;
        _interface.Interface().TakeDamage(_healValues[rarity] * -1);
        Debug.Log("回復" + _healValues[rarity]);
        Destroy(skeleton);
    }
}
