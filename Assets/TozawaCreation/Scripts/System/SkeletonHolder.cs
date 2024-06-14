using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using static SkeletonSummoner;
/// <summary>
/// �v���C���[�̕ێ�����X�P���g���̕ێ��󋵂��Ǘ��c������R���|�[�l���g
/// </summary>
public class SkeletonHolder : MonoBehaviour
{
    [SerializeField] SkeletonSummoner _skeletonSummoner;
    [SerializeField,Header("�ǂ̃����N�ɂǂꂭ�炢�̐��̉��ʃX�P���g�����K�v�Ȃ̂�")]
    int[] _requiredNumbers = new int[Enum.GetValues(typeof(SkeletonRarity)).Length];
    List<GameObject> _playersSkeletons = new List<GameObject>();
    int [] _rarityCounts = new int[Enum.GetValues(typeof(SkeletonRarity)).Length];

    private void Start()
    {
        _skeletonSummoner =  _skeletonSummoner ?? GameObject.Find("SkeletonSummoner").GetComponent<SkeletonSummoner>();
    }

    /// <summary>
    /// �X�P���g�������������Ƃ��ɓ����ɌĂ΂�A�i���������m�F���A�i�������𖞂����Ă���Έ��̃����N�̃X�P���g����������
    /// �f�ނƂȂ����X�P���g����j�󂷂�
    /// </summary>
    /// <param name="rarity">���A���e�B</param>
    /// <param name="pos">�����ʒu</param>
    /// <param name="skeleton">���������X�P���g��</param>
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
            Debug.Log(rarity + "��" + _requiredNumbers[(int)rarity] + "�̐��т�" + (SkeletonRarity)nextrarity + "�����I");
            _rarityCounts[(int)rarity] = 0;
        }
    }
}
