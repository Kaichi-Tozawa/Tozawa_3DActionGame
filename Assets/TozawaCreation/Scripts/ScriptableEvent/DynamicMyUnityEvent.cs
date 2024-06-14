using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Reflection;
using System;
using Unity.VisualScripting;

public static class DynamicMyUnityEvent 
{
    public static UnityEngine.Object GetDynamicTarget(this UnityEventBase unityEvent)
    {
        Type unityEventType = typeof(UnityEventBase);
        Assembly libassembly = Assembly.GetAssembly(unityEventType);
        Type invokableCallListType = libassembly.GetType("UnityEngine.Events.InvokableCallList");
        Type baseInvokableCallType = libassembly.GetType("UnityEngine.Events.BaseInvokableCall");
        Type listtype = typeof(List<>);
        Type baseInvokableCallListType = listtype.MakeGenericType(baseInvokableCallType);

        FieldInfo callsField = unityEventType
            .GetField("m_Calls",
            BindingFlags.Instance |
            BindingFlags.NonPublic);
        FieldInfo runtimeCallsField = invokableCallListType
            .GetField("m_RuntimeCalls",
            BindingFlags.Instance |
            BindingFlags.NonPublic);

        var calls = callsField.GetValue(unityEvent);
        var runtimecalls = runtimeCallsField.GetValue(calls);
        PropertyInfo callsProperty = baseInvokableCallListType.GetProperty("m_RuntimeCalls");

        return (UnityEngine.Object)callsProperty.GetValue(runtimecalls,null) ;
    }
}
