using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Player��AreaManager��ʂ��ĕ��ɂ̐�̂��s���Ă��鎞�ɁA
/// �i���x������UI�X���C�_�[���V�[����̕��ɂ�����ʒu�ɕ\������R���|�[�l���g
/// </summary>
public class AreaProgressUIManager : MonoBehaviour
{
    [SerializeField, Header("UI��\������Ώۂ̃I�u�W�F�N�g�̃g�����X�t�H�[��")]
    Transform _targetTransform;
    [SerializeField, Header("UI��\�������ʂ��ڂ��J����")]
    Camera _camera;
    Vector3 _targetWorldPos;
    Vector3 _targetScreenPos;
    [SerializeField,Header("�\������UI")]RectTransform _targetUI;
    [SerializeField,Header("�\������UI�̐e�L�����o�X")]RectTransform _parentUICanvas;
    private void Start()
    {
       _targetWorldPos  = _targetTransform.position;
    }
    private void Update()
    {
        WorldConvertToScreen();
        
    }

    /// <summary>
    /// ���[���h���W���X�N���[�����W�ɕϊ�
    /// </summary>
    private void WorldConvertToScreen()
    {
        _targetScreenPos = _camera.WorldToScreenPoint(_targetWorldPos);
    }
    /// <summary>
    /// �X�N���[�����W����UI�̃��[�J�����W�ւ̕ϊ�
    /// </summary>
    private void ScreenToUIRocalPoint()
    {

    }
}
