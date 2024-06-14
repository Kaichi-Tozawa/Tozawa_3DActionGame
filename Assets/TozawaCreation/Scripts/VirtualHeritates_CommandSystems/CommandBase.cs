using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandBase : MonoBehaviour
{
    protected virtual void SetCommand(GameObject skeleton) {}
    public void OnSetCommand(GameObject skeleton)
    {
        SetCommand(skeleton);
    }
}
