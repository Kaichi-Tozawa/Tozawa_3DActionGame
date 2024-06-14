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
    /// Enumの項目数から外れないように循環的に数値を返す
    /// </summary>
    /// <param name="currentnum">現在のステートナンバー</param>
    /// <returns>次のステートナンバー</returns>
    int Revolving(int currentnum)
    {
        currentnum++;
        return currentnum % Enum.GetValues(typeof(Tenum)).Length;
    }

    
}
