using UnityEngine;

interface INPCComander
{
    abstract IMove IMoveInstance();
    /// <summary>
    /// �d�v�W�I��ݒ�
    /// </summary>
    abstract void SetPriorityTarget(GameObject target);
    /// <summary>
    /// �h�q�ڕW��ݒ�
    /// </summary>
    abstract void DefenceContract(GameObject obj);
}
