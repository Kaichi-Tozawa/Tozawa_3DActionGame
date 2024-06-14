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

    public void SetUp(Transform originalroot)
    {
        CloneTransforms(originalroot,_rootBone);
    }

    private void CloneTransforms(Transform root, Transform clone)
    {
        foreach(Transform child in root)
        {
            Transform clonechild = clone.Find(child.name);
            if(clonechild != null)
            {
                clonechild.position = child.position;
                clonechild.rotation = child.rotation;
                CloneTransforms(child, clonechild);
            }
        }
    }
}
