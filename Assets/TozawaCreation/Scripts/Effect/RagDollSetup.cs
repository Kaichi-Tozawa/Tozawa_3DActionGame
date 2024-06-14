using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// 直前のモデルの動きに極力合わせた動きにラグトールのトランスフォームを置換するスクリプト
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
