using UnityEngine;
using UnityEngine.Events;
using System;
using System.Reflection;

public class UnityEventTest : MonoBehaviour
{
    public UnityEvent targetEvent; // 差し替えたいUnityEventを設定
    public GameObject prefab; // プレハブを設定

    /// <summary>
    /// 初期化時にシーン上の同名オブジェクトを見つけて、UnityEventのターゲットを差し替える。
    /// </summary>
    void Start()
    {
        ShowLogs();
    }
    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            //同名オブジェクトの取得
            GameObject sceneObject = GameObject.Find(prefab.name);
            if (sceneObject == null)
            {
                Debug.LogError($"No GameObject named {prefab.name} found in the scene");
                return;
            }
            ReplaceEventTargets(targetEvent, prefab, sceneObject);
            targetEvent.Invoke();
        }
    }
    /// <summary>
    /// UnityEventのすべてのリスナーのターゲットを指定されたシーンオブジェクトに差し替える。
    /// </summary>
    /// <param name="unityEvent">ターゲットを差し替えるUnityEvent</param>
    /// <param name="prefab">プレハブのゲームオブジェクト</param>
    /// <param name="sceneObject">シーン上のゲームオブジェクト</param>
    void ReplaceEventTargets(UnityEvent unityEvent, GameObject prefab, GameObject sceneObject)
    {
        //Unityイベントを全て差し替えるためにUnityEventの要素数取得
        int listenerCount = unityEvent.GetPersistentEventCount();

        for (int i = 0; i < listenerCount; i++)
        {
            //差し替え対象のターゲットObjectとメソッド名を取得
            UnityEngine.Object target = unityEvent.GetPersistentTarget(i);
            string methodName = unityEvent.GetPersistentMethodName(i);
            //UnityEventが空ではなく、シーン上のオブジェクトとプレハブの名前が一致するなら
            if (target != null && target.name == prefab.name)
            {
                //シーン上Objectから
                var sceneComponent = FindComponentWithMethod(sceneObject, methodName);
                if (sceneComponent != null)
                {
                    // シーンオブジェクトのメソッドをリスナーとして新たに設定
                    UnityAction action = Delegate.CreateDelegate(typeof(UnityAction), sceneComponent, methodName) as UnityAction;
                    unityEvent.SetPersistentListenerState(i, UnityEventCallState.Off); // 一時的にリスナーを無効化
                    unityEvent.RemoveAllListeners(); 
                    unityEvent.AddListener(action);
                    Debug.Log($"Replaced target of {methodName} from {target} to {sceneComponent}");
                }
                else
                {
                    Debug.LogError($"No matching component with method {methodName} found on {sceneObject.name}");
                }
            }
        }

    }

    /// <summary>
    /// 指定されたメソッド名を持つコンポーネントをシーンオブジェクトから検索する。
    /// </summary>
    /// <param name="sceneObject">検索対象のシーンオブジェクト</param>
    /// <param name="methodName">検索するメソッド名</param>
    /// <returns>見つかったコンポーネント、存在しない場合はnull</returns>
    MonoBehaviour FindComponentWithMethod(GameObject sceneObject, string methodName)
    {
        foreach (var component in sceneObject.GetComponents<MonoBehaviour>())
        {
            if (component.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) != null)
            {
                return component;
            }
        }
        return null;
    }
    void ShowLogs()
    {
        Debug.Log(targetEvent.GetPersistentTarget(0));
        Debug.Log(targetEvent.GetPersistentMethodName(0));
    }
}