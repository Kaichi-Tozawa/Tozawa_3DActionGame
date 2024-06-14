using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// スポナーで空オブジェクトの子に敵群を入れたオブジェクトを出すのでその親子関係を破棄してから自身を破棄するコンポーネント
/// </summary>
public class EnemyGroupDissolutioner : MonoBehaviour
{
    void Start()
    {
        this.gameObject.transform.DetachChildren();//親子関係をすべて解除
        Destroy(this.gameObject);
    }
}
