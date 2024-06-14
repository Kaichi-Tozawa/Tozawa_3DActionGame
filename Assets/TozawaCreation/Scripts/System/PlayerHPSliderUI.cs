using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPSliderUI : MonoBehaviour
{
    [SerializeField] InterfaceMediary<IHealth> _interfaceMIH;
    IHealth _playerIH;
    Slider _slider;
    
    void Start()
    {
        _slider = GetComponent<Slider>();
        _playerIH = _interfaceMIH.Interface();
    }

    void Update()
    {
        _slider.value = _playerIH.CurrentHealth();
    }
}
