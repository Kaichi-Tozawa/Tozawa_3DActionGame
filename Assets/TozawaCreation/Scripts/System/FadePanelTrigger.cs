using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadePanelTrigger : MonoBehaviour
{
    /// <summary>
    /// フェードアウトパネルのアニメーショントリガーから呼ぶ
    /// </summary>
    public void CanReStartTheSceneAnimTrigger()
    {
        RestartTheGame.canfade = true;
    }
}
