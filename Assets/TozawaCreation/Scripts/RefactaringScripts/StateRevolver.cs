using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateRevolver<Tenum> : IStateRevolver<Tenum> where Tenum : Enum
{
    Tenum _state;
    int _stateNum = 0;

    Tenum IStateRevolver<Tenum>.CurrentState()
    {
        return _state;
    }

    void IStateRevolver<Tenum>.NextState()
    {
        _stateNum = Revolving(_stateNum);
        _state = (Tenum)Enum.ToObject(typeof(Tenum),_stateNum);
    }
    /// <summary>
    /// Enum�̍��ڐ�����O��Ȃ��悤�ɏz�I�ɐ��l��Ԃ�
    /// </summary>
    /// <param name="currentnum">���݂̃X�e�[�g�i���o�[</param>
    /// <returns>���̃X�e�[�g�i���o�[</returns>
    int Revolving(int currentnum)
    {
        currentnum++;
        return currentnum % Enum.GetValues(typeof(Tenum)).Length;
    }

    
}
