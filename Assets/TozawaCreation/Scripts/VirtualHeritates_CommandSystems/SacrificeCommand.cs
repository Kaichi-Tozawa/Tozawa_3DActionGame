using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificeCommand : CommandBase
{
    [SerializeField] InterfaceMediary<IHealth> _interface;
    [SerializeField, Header("���ю��G�t�F�N�g")] GameObject _sacrificeEffect;
    [SerializeField, Header("�X�P���g����������ۂ̉񕜗ʂ��ア���ɓo�^���Ă�������")]
    int[] _healValues = new int[Enum.GetValues(typeof(SkeletonRarity)).Length];
    protected override void SetCommand(GameObject skeleton)
    {
        Instantiate(_sacrificeEffect, skeleton.transform.position, Quaternion.identity);
        int rarity = (int)skeleton.GetComponent<Rarity>().GetCurrentRarity;
        _interface.Interface().TakeDamage(_healValues[rarity] * -1);
        Debug.Log("��" + _healValues[rarity]);
        Destroy(skeleton);
    }
}
