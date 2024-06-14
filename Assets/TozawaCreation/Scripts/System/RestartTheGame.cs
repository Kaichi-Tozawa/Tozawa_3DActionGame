using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RestartTheGame : MonoBehaviour
{
    public static bool canfade = false;
    [SerializeField] GameObject _fadePanelGameobject;
    private void Start()
    {
        canfade = false;
    }
    public void ReStartTheGame()
    {
        StartCoroutine(StartFadeAndReRoadScene());
    }
    IEnumerator StartFadeAndReRoadScene()
    {
        _fadePanelGameobject.SetActive(true);
        yield return new WaitUntil(()=>canfade);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
