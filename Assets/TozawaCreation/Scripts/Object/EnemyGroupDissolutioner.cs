using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �X�|�i�[�ŋ�I�u�W�F�N�g�̎q�ɓG�Q����ꂽ�I�u�W�F�N�g���o���̂ł��̐e�q�֌W��j�����Ă��玩�g��j������R���|�[�l���g
/// </summary>
public class EnemyGroupDissolutioner : MonoBehaviour
{
    void Start()
    {
        this.gameObject.transform.DetachChildren();//�e�q�֌W�����ׂĉ���
        Destroy(this.gameObject);
    }
}
