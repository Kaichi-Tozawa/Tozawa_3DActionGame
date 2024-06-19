using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
public class StopEventHandler : MonoBehaviour
{
    public void StopAll()
    {
        foreach (var obj in FindObjectsOfType<GameObject>().Select(x => x.GetComponent<IPause>())) 
        {
            if (obj != null)
            {
                obj.Pause();
            }
        }
    }
  
    public void ReStart()
    {
        foreach (var obj in FindObjectsOfType<GameObject>().Select(x => x.GetComponent<IPause>()))
        {
            if (obj != null)
            {
                obj.Reboot();
            }
        }
    }
}
