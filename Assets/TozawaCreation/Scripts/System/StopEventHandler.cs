using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// �X�g�b�v�C�x���g�̔�����m�炳�ꂽ��V�[����S�Ă�IStoppable�C���^�[�t�F�[�X�̂������̂̕����������~�����āA
/// �����������������ꂽ���~���������Ď~�߂Ă������̓������ĊJ������R���|�[�l���g
/// ����̓I�u�U�[�o�[�p�^�[���������
/// </summary>
public class StopEventHandler : MonoBehaviour
{
    /// <summary>
    /// �V�[����S�Ă�IpauseAble�̂����I�u�W�F�N�g���~������
    /// UnityEvent����ĂԂƎQ�ƕ\������Ȃ��̂Œ���
    /// </summary>
    public void StopAll()
    {
        foreach (var obj in FindObjectsOfType<GameObject>().Select(x => x.GetComponent<IPause>())) 
        {
            if (obj != null)
            {
                obj.Pause();
            }
        }
    }
    /// <summary>
    /// �S�Ă̒�~���Ă����I�u�W�F�N�g���ĉғ�������
    /// UnityEvent����ĂԂƎQ�ƕ\������Ȃ��̂Œ���
    /// </summary>
    public void ReStart()
    {
        foreach (var obj in FindObjectsOfType<GameObject>().Select(x => x.GetComponent<IPause>()))
        {
            if (obj != null)
            {
                obj.Reboot();
            }
        }
    }
}
