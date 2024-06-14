using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitDetector : IUnitDetector
{
    GameObject _ownInstance;
    string _ownTag;
    string _targetTag;
    public  UnitDetector(string owntag, GameObject ownInstance)
    {
        _ownTag = owntag;
        _targetTag = _ownTag == "PlayerSide" ? "EnemySide" : "PlayerSide";
        _ownInstance = ownInstance;
    }
    Vector3 IUnitDetector.TargetDirection()
    {
        var dir = NearestTarget().transform.position - _ownInstance.transform.position;
        return dir;
    }

    Transform IUnitDetector.TargetTransform()
    {
        var pos = NearestTarget().transform;
        return pos;
    }
    float IUnitDetector.TargetDistance()
    {
        var distance = Vector3.Distance(_ownInstance.transform.position,NearestTarget().transform.position);
        if (distance < 0) 
        {
            distance = 0;
        }
        return distance;
    }

    /// <summary>
    /// 敵対タグから距離を比較し最も近いゲームオブジェクトを_targetGameObjectに代入する
    /// </summary>
    GameObject NearestTarget()
    {
        GameObject mostNearObject =default;
        try
        {
            mostNearObject = GameObject.FindGameObjectsWithTag(_targetTag)
                .Aggregate((current, next) =>
                Vector3.Distance(current.transform.position, _ownInstance.transform.position)
                < Vector3.Distance(next.transform.position, _ownInstance.transform.position) ? current : next);
            
        }
        catch (System.InvalidOperationException)
        {
            mostNearObject = _ownInstance;
        }
        return mostNearObject;
    }
}
