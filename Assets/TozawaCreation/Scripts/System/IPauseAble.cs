using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �|�[�Y�@�\�C���^�[�t�F�[�X
/// </summary>
interface IPauseAble
{
    /// <summary>
    /// �|�[�Y���ɓ����Ăق����Ȃ����̑S�Ă��~�����Ă�������
    /// </summary>
    void Pause();
    /// <summary>
    /// �|�[�Y���Ɏ~�߂Ă������̂�S�Č��ʂ�ɓ������ĊJ�����ĉ�����
    /// </summary>
    void Reboot();
}

