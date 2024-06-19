using UnityEngine;
using System.Linq;
using System;

public static class AnimParameterSetting 
{
    /// <summary>
    /// アニメーターのSet関数代替
    /// </summary>
    /// <param name="anim">アニメーター</param>
    /// <param name="paramname">パラメーター名</param>
    /// <param name="type">値</param>
    public static void SetParam<T>(ref Animator anim,string paramname,T type) 
    {
        switch (type) 
        {
            case float:
                anim.SetFloat(paramname,(float)(object)type);
                break;
            case int: 
                anim.SetInteger(paramname,(int)(object)type);
                break;
            case bool:
                anim.SetBool(paramname,(bool)(object)type);
                break;
            default:
                anim.SetTrigger(paramname);
                break;
        }
    }

    /// <summary>
    /// トリガー用オーバーロード
    /// </summary>
    public static void SetParam(ref Animator anim,string paramname)
    {
        SetParam(ref anim, paramname, anim);
    }

    /// <summary>
    /// 指定したパラメーター名が存在しなかった時に通知を兼ねたいのでAnyは使わない
    /// </summary>
    public static  bool CheckExist(Animator anim, string paramname)
    {
        try
        {
            var isnameExit = anim.parameters.First(p => p.name == paramname);
        }
        catch (InvalidOperationException)
        {
            Debug.LogError("指定された名前のパラメーターは存在しません");
            return false;
        }
        return true;
    }

    
}
