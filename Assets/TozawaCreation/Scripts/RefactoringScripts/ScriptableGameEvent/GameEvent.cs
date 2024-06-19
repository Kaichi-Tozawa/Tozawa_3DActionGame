using System.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Reflection;
[CreateAssetMenu(fileName ="EventDeta",menuName ="Create EventDeta_ScriptableObject")]
public class GameEvent : ScriptableObject,IGameEvent
{
    readonly List<GameEventWaiting> _eventWaitings = new List<GameEventWaiting>();
    [SerializeField,Header( "イベント関連プレハブをアタッチしてください。")]
    GameObject[] _eventPrefabs;
    [TextArea, Header("イベントの補足説明"),SerializeField] string _descriptionText;
    [SerializeField,Header(
        "⚠引数のない関数だけ定義してください⚠" + "\n" +
        "ScriptableObject側で登録がない場合は" +"\n"+
        "EventWaiting側に登録されたUnityEventを実行します" + "\n" +
        "処理がなくても枠がある場合はGameEvent側を実行しようとするので注意" + "\n" +
        "EventWaiting側を実行したければEmptyにすること")]
    UnityEvent _staticevents =null;
    /// <summary>
    /// イベント発生通知
    /// </summary>
    public void EventOccurrence()
    {
        foreach (var waiting in _eventWaitings)
        {
            try
            {
                if(_staticevents.GetPersistentEventCount() == 0)
                {
                    waiting.OnEventOccur();
                }
                else
                {
                    waiting.OnEventOccur(ConvertEvent(_staticevents));
                }
            }
            catch(System.Exception e) 
            {
                UnityEngine.Debug.LogError(e.StackTrace+_staticevents +_descriptionText);
                throw;
            }
        }
    }
    UnityEvent ConvertEvent(UnityEvent staticEvent)
    {
        UnityEvent dynamicEvent = new UnityEvent();
        dynamicEvent.RemoveAllListeners(); 
        int length = staticEvent.GetPersistentEventCount();
        for (int i = 0; i < length; ++i)
        {
            UnityAction action = Delegate.CreateDelegate
                (typeof(UnityAction),
                SceneTargetObject(i, staticEvent),
                EventMethodInfo(i,staticEvent)) as UnityAction;
            dynamicEvent.AddListener(action);
        }
        return dynamicEvent;
    }
    object SceneTargetObject(int count, UnityEvent staticEvent)
    {
        if(staticEvent.GetPersistentTarget(count).GetType()==this.GetType())
        {
            return staticEvent.GetPersistentTarget(count);
        }
        try
        {
            var sceneobj = GameObject.Find(staticEvent.GetPersistentTarget(count).name);
            var obj = sceneobj.GetComponent<MonoBehaviour>();
            return obj;
        }
        catch(System.Exception e) 
        {
            UnityEngine.Debug.Log(e + "\n" +
                "イベントに定義されたプレハブと同名のオブジェクトがシーンに存在しません");
            return null;
        }
    }
    MethodInfo EventMethodInfo(int count,UnityEvent staticEvent)
    {
        var methods = staticEvent.GetPersistentTarget(count).GetType()
            .GetMethods(BindingFlags.Public |
            BindingFlags.Instance |
            BindingFlags.FlattenHierarchy);
        var info = methods.FirstOrDefault(c =>
        {
            return
            c.Name == staticEvent.GetPersistentMethodName(count) &&
            c.GetParameters().Length == 0;
        });

        return info;
    }
    public void Contract(GameEventWaiting waitlist)
    {
        _eventWaitings.Add(waitlist);
    }

    public void Termination(GameEventWaiting waitlist)
    {
        _eventWaitings.Remove(waitlist);
    }
}