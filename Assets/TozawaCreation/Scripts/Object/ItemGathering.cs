using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// リジッドボディを持たせたくないがアイテム側の移動だけでは衝突判定が起こらないために取得とみなす距離で管理
/// </summary>
public class ItemGathering : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 2f;
    [SerializeField] string _targetName;
    Transform _targetPos = null;
    float _initialYPos;
    private void Start()
    {
        _initialYPos = this.transform.position.y;
    }
    private void Update()
    {
        if(_targetPos)
        {
            var dir = Vector3.Lerp(this.transform.position, _targetPos.position, _moveSpeed * Time.deltaTime);
            dir.y = _initialYPos;
            this.transform.position = dir;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name == _targetName)
        {
            _targetPos = other.transform;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerSide"))
        {
            if (collision.gameObject.name == _targetName)
            {
                var item = collision.gameObject.GetComponent<ItemGatheringCollectioner>();
                item.GetItem();
                Destroy(this.gameObject);
            }
            else
            {
                this.GetComponent<BoxCollider>().enabled = false;
                Invoke(nameof(ReActiveCollision),_moveSpeed);
            }
        }
    }
    private void ReActiveCollision()
    {
        this.GetComponent<BoxCollider>().enabled = true;
    }
}
