using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestScript : MonoBehaviour
{
    [SerializeField] string text = "デフォはプレハブ";
    [SerializeField] UnityEvent _onTest;
    public void Test()
    {
        Debug.Log(text);
        _onTest.Invoke();
    }
    public void Test2()
    {
        this.gameObject.SetActive(false);
    }
    public void Test3(string text)
    {
        Debug.Log(text);
    }
}
