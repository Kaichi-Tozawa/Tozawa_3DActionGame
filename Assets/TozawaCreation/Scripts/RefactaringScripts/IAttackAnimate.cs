using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �U���A�j���[�V�����̋������`����C���^�[�t�F�[�X
/// </summary>
 interface IAttackAnimate
{
    /// <summary>
    /// �U���A�j���[�V�����X�e�[�g�ɑJ�ڂ���t���O
    /// </summary>
    /// <param name="paramname">�U���p�����[�^�[�̖��O</param>
    /// <param name="onoff">�u�[���l</param>
    abstract void SetAttackBool(string paramname, bool onoff);
}
