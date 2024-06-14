using UnityEngine;

interface IUnitDetector
{
    /// <returns>–Ú•WˆÊ’u</returns>
    abstract Transform TargetTransform();
    abstract Vector3 TargetDirection();
    abstract float TargetDistance();
}
