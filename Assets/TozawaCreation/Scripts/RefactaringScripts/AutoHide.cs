using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoHide : MonoBehaviour
{
    [SerializeField, Header("”ñ•\Ž¦‚É‚È‚éŽžŠÔ")] float _graceTime;
    
    void OnEnable()
    {
        Invoke(nameof(HideSelf), _graceTime);
    }
    void HideSelf()
    {
        this.gameObject.SetActive(false);
    }
}
