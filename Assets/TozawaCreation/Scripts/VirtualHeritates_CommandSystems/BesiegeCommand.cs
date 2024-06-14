using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BesiegeCommand : CommandBase
{
    [SerializeField] GameObject _besiegeTarget;
    protected override void SetCommand(GameObject skeleton)
    {
        skeleton.GetComponent<INPCComander>().SetPriorityTarget(_besiegeTarget);
    }
}
