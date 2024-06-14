using UnityEngine;

interface INPCComander
{
    abstract IMove IMoveInstance();
    /// <summary>
    /// 重要標的を設定
    /// </summary>
    abstract void SetPriorityTarget(GameObject target);
    /// <summary>
    /// 防衛目標を設定
    /// </summary>
    abstract void DefenceContract(GameObject obj);
}
