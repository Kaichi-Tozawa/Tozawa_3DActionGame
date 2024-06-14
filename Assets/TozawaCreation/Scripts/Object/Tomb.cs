using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tomb : MonoBehaviour
{
    [SerializeField] GameEvent _skeletonSummonEvent;
    [SerializeField] Transform _spawnPos;
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name=="Player")
        {
            _skeletonSummonEvent.EventOccurrence();
            Destroy(this.gameObject);
        }
    }
}
