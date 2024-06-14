using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleSpeedAfterDeath : MonoBehaviour
{
    [SerializeField] float _quickSpeedScale =2;
    private void Start()
    {
        Time.timeScale = 1;
    }
    private void OnDestroy()
    {
        Time.timeScale = _quickSpeedScale;
    }
}
