using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
/// <summary>
/// èüîsÇ™åàÇ‹Ç¡ÇΩå„Ç…ãNÇ±Ç¡ÇΩÉCÉxÉìÉgÇ…âûÇ∂ÇƒèàóùÇçsÇ§Ç‡ÇÃ
/// </summary>
public class Arbitrator : MonoBehaviour
{
    string _resulttext ;
    [SerializeField] UnityEvent _onGameFinished;
    [SerializeField] float _graceTime = 1.5f;
    public void PostWarProcessing(bool isWin)
    {
        _resulttext = isWin ? "èü\nóò" : "îs\nñk";
        Domination(isWin);
        SetResultText(_resulttext);
        Invoke(nameof(DoUnityEvent), _graceTime);
        this.enabled = false;
    }
    public void SetResultText(string t)
    {
        _resulttext = t;
    }
    public void SetResultTextOnUI(Text ui)
    {
        ui.text = _resulttext;
    }
    public void Domination(bool iswin)
    {
        var objcts = FindObjectsOfType(typeof(GameObject));
        foreach (var obj in objcts)
        {
            if (obj.GameObject().TryGetComponent<Animator>(out var anim))
            {
                InstructAnimation(obj, anim, iswin);
            }
            if (obj.GameObject().TryGetComponent<Rigidbody>(out var rb))
            {
                rb.velocity = Vector3.zero;
            }
            if (obj.GameObject().TryGetComponent<UnitCreator>(out var uc))
            {
                StopCompornent<UnitCreator>(obj);
                obj.GameObject().SetActive(false);
            }
            if (obj.GameObject().TryGetComponent<TimeLimit>(out var tl))
            {
                StopCompornent<TimeLimit>(obj);
                
            }
            if(obj.GameObject().TryGetComponent<AreaManager>(out var am))
            {
                StopCompornent<AreaManager>(obj);
            }
            if (obj.GameObject().TryGetComponent<NPCComander>(out var npc))
            {
                StopCompornent<NPCComander>(obj);
                StopCompornent<WallAvoid>(obj);
            }
        }
    }
    void DoUnityEvent()
    {
        _onGameFinished?.Invoke();
    }

    public void PlayerSideWin()
    {
        PostWarProcessing(true);
    }
    public void PlayerSideLose()
    {
        PostWarProcessing(false);
    }
    void InstructAnimation(Object obj,Animator anim,bool iswin)
    {
        string animname = iswin ? "LoseIdle" : "ResultIdle";
        var tag = obj.GameObject().tag;
        if (tag == "PlayerSide")
        {
            obj.GetComponent<Animator>().Play("ResultIdle");
            StopCompornent<PlayerInput>(obj);
        }
        else if (tag == "EnemySide")
        {
            obj.GetComponent<Animator>().Play(animname);
        }
        StopCompornent<NPCComander>(obj);
    }
    public void StopCompornent<T>(Object obj)where T : Behaviour
    {
        var compornent = obj.GetComponent<T>();
        if (compornent != null)
        {
            compornent.enabled = false;
        }
    }
}
