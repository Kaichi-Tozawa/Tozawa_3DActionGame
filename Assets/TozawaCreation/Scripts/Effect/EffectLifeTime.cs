using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// パーティクルやラグドールなどを任意の時間で消滅させるコンポーネント
/// </summary>
public class EffectLifeTime : MonoBehaviour,IPause
{
    [SerializeField] float _lifeTime;
    float _erapsedTime = 0;
    private void Start()
    {
        Invoke(nameof(DestroyThis), _lifeTime);
    }
    private void Update()
    {
        _erapsedTime += Time.deltaTime;
    }
    public void DestroyThis()
    {
        Destroy(this.gameObject);
    }

    public void Pause()
    {
        CancelInvoke(nameof(DestroyThis));
    }

    public void Reboot()
    {
        float time = _lifeTime - _erapsedTime;
        if (time < 0)
        {
            time = 0;
        }
        Invoke(nameof(DestroyThis), time);
    }
}