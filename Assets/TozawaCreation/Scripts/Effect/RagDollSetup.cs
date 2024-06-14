using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// ���O�̃��f���̓����ɋɗ͍��킹�������Ƀ��O�g�[���̃g�����X�t�H�[����u������X�N���v�g
/// </summary>
public class RagDollSetup : MonoBehaviour
{
    [SerializeField] Transform _rootBone;
    
    public void SetUp(Transform originalrootbone)
    {
        CloneTransforms(originalrootbone,_rootBone);
    }

    private void CloneTransforms(Transform root, Transform clone)
    {
        foreach(Transform child in root)
        {
            Transform clonechilde = clone.Find(child.name);
            if(clonechilde != null)
            {
                clonechilde.position = child.position;
                clonechilde.rotation = child.rotation;
                CloneTransforms(child, clonechilde);
            }
        }
    }
}
