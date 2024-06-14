using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rarity : MonoBehaviour
{
    [SerializeField] SkeletonRarity currentRarity;
    public SkeletonRarity GetCurrentRarity
    {  get { return currentRarity; } }

}
