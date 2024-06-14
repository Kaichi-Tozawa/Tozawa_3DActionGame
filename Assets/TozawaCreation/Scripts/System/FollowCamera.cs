using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameSystem
{
    /// <summary>
    /// �v���C���[�ɒǏ]����J�����̃R���|�[�l���g
    /// </summary>
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] Transform _followTargetPos = default;
        [SerializeField, Range(1, MAX_SCALE),Header("�_���s���O��𒲐߂��Ă�������")] float _dampingScale = 4f;
        [SerializeField,Header("�v���C���[����Unit��������Ȃ��ꍇ�̏���")] UnityEvent _onNoPlayerSideUnitDoesNotExist;
        [SerializeField, Header("�ǐՑΏۂ̃^�O")] string _targetTag;
        Vector3 _cameraOffset = default;
        const float MAX_SCALE = 5;
        void Start()
        {
            if (!_followTargetPos)
            {
                SearchUnit(_targetTag);
            }
            _cameraOffset = transform.position - _followTargetPos.position;
        }

        private void FixedUpdate()
        {
            if (_followTargetPos != null) 
            {
                var cameraPos = _followTargetPos.position + _cameraOffset;
                transform.position = Vector3.Slerp(transform.position, cameraPos, _dampingScale * Time.deltaTime);
            }
            else
            {
                SearchUnit(_targetTag);
            }
        }
        void SearchUnit(string unitTag)
        {
            try
            {
                _followTargetPos = GameObject.FindGameObjectWithTag(unitTag).transform;
            }
            catch (NullReferenceException)
            {
                _onNoPlayerSideUnitDoesNotExist.Invoke();
                this.enabled = false;
                return;
            }
        }
    }
}

