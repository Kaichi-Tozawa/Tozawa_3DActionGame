using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
/// <summary>
/// �o�[�`�����X�e�B�b�N���Ǘ�����R���|�[�l���g
/// �����I�u�W�F�N�g�̓o�[�`�����X�e�B�b�N�̊����
/// </summary>
public class VirtualStick : MonoBehaviour,IVirtualStick
{
    [SerializeField] GameObject _stick = default;
    
    Vector2 _localPoint = Vector2.zero;
    Vector2 _startPos = Vector2.zero; 
    Canvas _canvas = default;
    RectTransform _canvasRT;//�e�L�����o�X�̃��N�g�g�����X�t�H�[��
    RectTransform _virtialStickRT;//���I�u�W�F�N�g�̃��N�g�g�����X�t�H�[��
    RectTransform _stickRT;//�X�e�B�b�N���ʂ̃��N�g�g�����X�t�H�[��
    float _stickRadius ;
    float _horizonInput = default;
    float _verticalInput = default;
    bool _isPressed = false;�@
    bool _isPause = false;

    void Start()
    {
        _canvas = GetComponentInParent<Canvas>();
        _canvasRT = _canvas.GetComponent<RectTransform>();
        _virtialStickRT = GetComponent<RectTransform>();
        _stickRT = _stick.GetComponent<RectTransform>();
        _stickRadius = _virtialStickRT.sizeDelta.x/2;//���͈͂�UI�T�C�Y�̔��a�Ƃ���
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
    /// ��ʏ�̃^�b�`���ꂽ�n�_����UI���W�Ƃ��Ď擾����
    /// </summary>
    void GetTouchedPoint()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle
            (_canvasRT,Input.mousePosition,_canvas.worldCamera,out _localPoint);
    }

    /// <summary>
    /// �^�b�v���ꂽ�ʒu�Ƀo�[�`�����X�e�B�b�N���ړ�������
    /// </summary>
    void MovetoTapPoint()
    {
        _virtialStickRT.anchoredPosition = _localPoint;
        _startPos = _localPoint;
    }

    /// <summary>
    /// �h���b�O���̋���
    /// </summary>
    void OnDrag()
    {
        //�}�E�X�̈ʒu���L�����o�X����W�ɕϊ�
        RectTransformUtility.ScreenPointToLocalPointInRectangle
            (_canvasRT, Input.mousePosition, _canvas.worldCamera, out Vector2 _stickPos);
        _horizonInput = GetInputValue(_stickPos.x, _startPos.x);
        _verticalInput = GetInputValue(_stickPos.y, _startPos.y);
        float dis = Vector2.Distance(_startPos, _stickPos);
        _stickRT.anchoredPosition = (dis < _stickRadius)
            ? _stickPos : _startPos + ((_stickPos - _startPos) / dis * _stickRadius);
    }

    /// <summary>
    /// ���͂̎擾
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
