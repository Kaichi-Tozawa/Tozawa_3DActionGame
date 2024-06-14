using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// PlayerがAreaManagerを通して兵舎の占領を行っている時に、
/// 進捗度を示すUIスライダーをシーン上の兵舎がある位置に表示するコンポーネント
/// </summary>
public class AreaProgressUIManager : MonoBehaviour
{
    [SerializeField, Header("UIを表示する対象のオブジェクトのトランスフォーム")]
    Transform _targetTransform;
    [SerializeField, Header("UIを表示する画面を移すカメラ")]
    Camera _camera;
    Vector3 _targetWorldPos;
    Vector3 _targetScreenPos;
    [SerializeField,Header("表示するUI")]RectTransform _targetUI;
    [SerializeField,Header("表示するUIの親キャンバス")]RectTransform _parentUICanvas;
    private void Start()
    {
       _targetWorldPos  = _targetTransform.position;
    }
    private void Update()
    {
        WorldConvertToScreen();
        
    }

    /// <summary>
    /// ワールド座標をスクリーン座標に変換
    /// </summary>
    private void WorldConvertToScreen()
    {
        _targetScreenPos = _camera.WorldToScreenPoint(_targetWorldPos);
    }
    /// <summary>
    /// スクリーン座標からUIのローカル座標への変換
    /// </summary>
    private void ScreenToUIRocalPoint()
    {

    }
}
