using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ボスが倒されたときまたはプレイヤー陣営が倒されたときに呼ばれるが
/// </summary>
public class GameConclusionSlowMotion : MonoBehaviour
{
    [SerializeField] float _slowmotionTime;
    [SerializeField] float _valueTimeScale;

    public void SlowMotion()
    {
        var camera = Camera.main.gameObject.GetComponent<CameraShake>();
        camera.StartSlowmotion(_valueTimeScale, _slowmotionTime);
    }
    
}
