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
    ///// <summary>
    ///// ���Ԃɉ����ăX�e�[�g��i�߂���n���ꂽ�A�N�V���������s����R���[�`��
    ///// </summary>
    ///// <param name="action">�Ȃ񂩂�����������</param>
    ///// <param name="operatingtime">���̃X�e�[�g�̉ғ�����</param>
    ///// <returns></returns>
    //abstract IEnumerator StateBehaviorTimerCorutine(Action action,float operatingtime);
}
