using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// ストップイベントの発生を知らされたらシーン上全てのIStoppableインターフェースのついた物体の物理挙動を停止させて、
/// 解除条件が満たされたら停止を解除して止めていた物の動きを再開させるコンポーネント
/// 今回はオブザーバーパターンを避ける
/// </summary>
public class StopEventHandler : MonoBehaviour
{
    /// <summary>
    /// シーン上全てのIpauseAbleのついたオブジェクトを停止させる
    /// UnityEventから呼ぶと参照表示されないので注意
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
    /// 全ての停止していたオブジェクトを再稼働させる
    /// UnityEventから呼ぶと参照表示されないので注意
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
