using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// レンダラーについているマテリアルのタイリングやオフセットを動かし続けるコンポーネント
/// </summary>
public class MaterialSufaceAnimation : MonoBehaviour,IPause
{
    [SerializeField] float _offsetsY = 0.7f;
    Renderer _currentRenderer;
    float _value = 0;
    Vector2 _offset;
    Vector2 _tiling;
    bool _active = true ;
    private void Start()
    {
        _currentRenderer = GetComponent<Renderer>();
        _value = 0;
    }
    private void FixedUpdate()
    {
        if (_active)
        {
            _value += Time.deltaTime;
            _offset = new Vector2(_value, _offsetsY);
            _tiling = new Vector2(_value, _offsetsY);
            _currentRenderer.material.mainTextureOffset = _offset;
        }
    }

    void IPause.Pause()
    {
        _active = false;
    }

    void IPause.Reboot()
    {
        _active |= false;
    }
}
