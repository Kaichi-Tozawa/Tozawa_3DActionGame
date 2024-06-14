using UnityEngine;

interface IUnitDetector
{
    /// <returns>�ڕW�ʒu</returns>
    abstract Transform TargetTransform();
    abstract Vector3 TargetDirection();
    abstract float TargetDistance();
}
