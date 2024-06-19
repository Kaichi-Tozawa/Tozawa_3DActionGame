using UnityEngine;
using System.Linq;
using System;

public static class AnimParameterSetting 
{
    /// <summary>
    /// �A�j���[�^�[��Set�֐����
    /// </summary>
    /// <param name="anim">�A�j���[�^�[</param>
    /// <param name="paramname">�p�����[�^�[��</param>
    /// <param name="type">�l</param>
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
    /// �g���K�[�p�I�[�o�[���[�h
    /// </summary>
    public static void SetParam(ref Animator anim,string paramname)
    {
        SetParam(ref anim, paramname, anim);
    }

    /// <summary>
    /// �w�肵���p�����[�^�[�������݂��Ȃ��������ɒʒm�����˂����̂�Any�͎g��Ȃ�
    /// </summary>
    public static  bool CheckExist(Animator anim, string paramname)
    {
        try
        {
            var isnameExit = anim.parameters.First(p => p.name == paramname);
        }
        catch (InvalidOperationException)
        {
            Debug.LogError("�w�肳�ꂽ���O�̃p�����[�^�[�͑��݂��܂���");
            return false;
        }
        return true;
    }

    
}
