using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIPlayer : MonoBehaviour
{
    [SerializeField] private TMP_Text clipUI;
    [SerializeField] private RawImage iconUI;
    public void UpdateUIPlayerClip(int PlayerClip,int CountBullets, RawImage icon)
    {
         clipUI.text  =  $"X{PlayerClip}/{CountBullets}"; 
         iconUI.texture = icon.texture;
         
    }
}
