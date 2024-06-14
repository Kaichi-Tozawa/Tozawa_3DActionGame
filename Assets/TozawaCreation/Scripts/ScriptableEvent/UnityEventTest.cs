using UnityEngine;
using UnityEngine.Events;
using System;
using System.Reflection;

public class UnityEventTest : MonoBehaviour
{
    public UnityEvent targetEvent; // �����ւ�����UnityEvent��ݒ�
    public GameObject prefab; // �v���n�u��ݒ�

    /// <summary>
    /// ���������ɃV�[����̓����I�u�W�F�N�g�������āAUnityEvent�̃^�[�Q�b�g�������ւ���B
    /// </summary>
    void Start()
    {
        ShowLogs();
    }
    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            //�����I�u�W�F�N�g�̎擾
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
    /// UnityEvent�̂��ׂẴ��X�i�[�̃^�[�Q�b�g���w�肳�ꂽ�V�[���I�u�W�F�N�g�ɍ����ւ���B
    /// </summary>
    /// <param name="unityEvent">�^�[�Q�b�g�������ւ���UnityEvent</param>
    /// <param name="prefab">�v���n�u�̃Q�[���I�u�W�F�N�g</param>
    /// <param name="sceneObject">�V�[����̃Q�[���I�u�W�F�N�g</param>
    void ReplaceEventTargets(UnityEvent unityEvent, GameObject prefab, GameObject sceneObject)
    {
        //Unity�C�x���g��S�č����ւ��邽�߂�UnityEvent�̗v�f���擾
        int listenerCount = unityEvent.GetPersistentEventCount();

        for (int i = 0; i < listenerCount; i++)
        {
            //�����ւ��Ώۂ̃^�[�Q�b�gObject�ƃ��\�b�h�����擾
            UnityEngine.Object target = unityEvent.GetPersistentTarget(i);
            string methodName = unityEvent.GetPersistentMethodName(i);
            //UnityEvent����ł͂Ȃ��A�V�[����̃I�u�W�F�N�g�ƃv���n�u�̖��O����v����Ȃ�
            if (target != null && target.name == prefab.name)
            {
                //�V�[����Object����
                var sceneComponent = FindComponentWithMethod(sceneObject, methodName);
                if (sceneComponent != null)
                {
                    // �V�[���I�u�W�F�N�g�̃��\�b�h�����X�i�[�Ƃ��ĐV���ɐݒ�
                    UnityAction action = Delegate.CreateDelegate(typeof(UnityAction), sceneComponent, methodName) as UnityAction;
                    unityEvent.SetPersistentListenerState(i, UnityEventCallState.Off); // �ꎞ�I�Ƀ��X�i�[�𖳌���
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
    /// �w�肳�ꂽ���\�b�h�������R���|�[�l���g���V�[���I�u�W�F�N�g���猟������B
    /// </summary>
    /// <param name="sceneObject">�����Ώۂ̃V�[���I�u�W�F�N�g</param>
    /// <param name="methodName">�������郁�\�b�h��</param>
    /// <returns>���������R���|�[�l���g�A���݂��Ȃ��ꍇ��null</returns>
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