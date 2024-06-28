using System;
using System.Collections;

interface IStateRevolver<T>where T : Enum
{
    /// <summary>
    /// ���݂̃X�e�[�g��Ԃ�
    /// </summary>
    abstract T CurrentState();
    /// <summary>
    /// ���̃X�e�[�g��
    /// </summary>
    abstract void NextState();
}
