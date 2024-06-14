using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoHide : MonoBehaviour
{
    [SerializeField, Header("��\���ɂȂ鎞��")] float _graceTime;
    
    void OnEnable()
    {
        Invoke(nameof(HideSelf), _graceTime);
    }
    void HideSelf()
    {
        this.gameObject.SetActive(false);
    }
}
