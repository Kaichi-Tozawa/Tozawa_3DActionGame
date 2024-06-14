using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameSystem
{
    /// <summary>
    /// プレイヤーに追従するカメラのコンポーネント
    /// </summary>
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] Transform _followTargetPos = default;
        [SerializeField, Range(1, MAX_SCALE),Header("ダンピング具合を調節してください")] float _dampingScale = 4f;
        [SerializeField,Header("プレイヤー側のUnitが見つからない場合の処理")] UnityEvent _onNoPlayerSideUnitDoesNotExist;
        [SerializeField, Header("追跡対象のタグ")] string _targetTag;
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

