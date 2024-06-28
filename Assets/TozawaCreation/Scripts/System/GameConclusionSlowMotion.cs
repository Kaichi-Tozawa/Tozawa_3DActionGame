using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �{�X���|���ꂽ�Ƃ��܂��̓v���C���[�w�c���|���ꂽ�Ƃ��ɌĂ΂�邪
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
