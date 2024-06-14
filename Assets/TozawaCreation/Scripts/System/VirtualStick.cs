using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
/// <summary>
/// バーチャルスティックを管理するコンポーネント
/// 装着オブジェクトはバーチャルスティックの基幹部分
/// </summary>
public class VirtualStick : MonoBehaviour,IVirtualStick
{
    [SerializeField] GameObject _stick = default;
    
    Vector2 _localPoint = Vector2.zero;
    Vector2 _startPos = Vector2.zero; 
    Canvas _canvas = default;
    RectTransform _canvasRT;//親キャンバスのレクトトランスフォーム
    RectTransform _virtialStickRT;//当オブジェクトのレクトトランスフォーム
    RectTransform _stickRT;//スティック部位のレクトトランスフォーム
    float _stickRadius ;
    float _horizonInput = default;
    float _verticalInput = default;
    bool _isPressed = false;　
    bool _isPause = false;

    void Start()
    {
        _canvas = GetComponentInParent<Canvas>();
        _canvasRT = _canvas.GetComponent<RectTransform>();
        _virtialStickRT = GetComponent<RectTransform>();
        _stickRT = _stick.GetComponent<RectTransform>();
        _stickRadius = _virtialStickRT.sizeDelta.x/2;//可動範囲をUIサイズの半径とする
    }
    void Update()
    {
        if(_isPause) { return; }
        if (Input.GetButtonDown("Fire1"))
        {
            GetTouchedPoint();
            MovetoTapPoint();
            _isPressed = true;
        }
        if(Input.GetButtonUp("Fire1"))
        {
            _isPressed = false;
            _stickRT.anchoredPosition = _startPos;
        }
        if(_isPressed)
        {
            OnDrag();
        }
    }

    /// <summary>
    /// 画面上のタッチされた地点情報をUI座標として取得する
    /// </summary>
    void GetTouchedPoint()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle
            (_canvasRT,Input.mousePosition,_canvas.worldCamera,out _localPoint);
    }

    /// <summary>
    /// タップされた位置にバーチャルスティックを移動させる
    /// </summary>
    void MovetoTapPoint()
    {
        _virtialStickRT.anchoredPosition = _localPoint;
        _startPos = _localPoint;
    }

    /// <summary>
    /// ドラッグ中の挙動
    /// </summary>
    void OnDrag()
    {
        //マウスの位置をキャンバス上座標に変換
        RectTransformUtility.ScreenPointToLocalPointInRectangle
            (_canvasRT, Input.mousePosition, _canvas.worldCamera, out Vector2 _stickPos);
        _horizonInput = GetInputValue(_stickPos.x, _startPos.x);
        _verticalInput = GetInputValue(_stickPos.y, _startPos.y);
        float dis = Vector2.Distance(_startPos, _stickPos);
        _stickRT.anchoredPosition = (dis < _stickRadius)
            ? _stickPos : _startPos + ((_stickPos - _startPos) / dis * _stickRadius);
    }

    /// <summary>
    /// 入力の取得
    /// </summary>
    float GetInputValue(float nowPos,float startPos)
    {
        var distance = nowPos - startPos;
        var value = (Mathf.Abs(distance) < _stickRadius) 
            ? distance / _stickRadius : Mathf.Abs(distance) / distance;
        return value;
    }
    float IVirtualStick.HorizonInput()
    {
        return _horizonInput;
    }
    float IVirtualStick.VerticalInput()
    {
        return _verticalInput;
    }
    bool IVirtualStick.Pressed()
    {
        return _isPressed;
    }
    void IVirtualStick.DisAble()
    {
        
    }

    public void Pause()
    {
        _isPause = true;
    }

    public void Reboot()
    {
        _isPause = false;
        _isPressed = false;
        _stickRT.anchoredPosition = _startPos;
    }
}
