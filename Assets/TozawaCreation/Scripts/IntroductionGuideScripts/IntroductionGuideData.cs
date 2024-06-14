using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "IntorductionGuideDeta", menuName = "Create IntroductionGuideDeta_ScriptableObject")]
public class IntroductionGuideData : ScriptableObject
{
    [SerializeField] Sprite _Image;
    [SerializeField] string _introductionText;
    public Sprite GuideImage()
    {
        return _Image;
    }
    public string GuideText() 
    {
        return _introductionText;
    }
}
