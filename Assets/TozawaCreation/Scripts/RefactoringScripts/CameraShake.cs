using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Animator _cameraAnim;
    private void Start()
    {
        _cameraAnim = GetComponent<Animator>();
    }
    public void StartSlowmotion(float slowtimeScale, float timewhile)
    {
        StartCoroutine(SlowmotionCorutine(slowtimeScale, timewhile));
    }
    IEnumerator SlowmotionCorutine(float slowtimeScale,float timewhile)
    {
        _cameraAnim.SetTrigger("CameraShake");
        Time.timeScale = slowtimeScale;
        yield return new WaitForSecondsRealtime(timewhile);
        Time.timeScale = 1;
    }
}
