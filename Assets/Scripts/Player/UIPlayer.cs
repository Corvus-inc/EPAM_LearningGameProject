using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIPlayer : MonoBehaviour
{
    public PlayerLevel MyLevel { get; set; }
    
    [SerializeField] private TMP_Text clipUI;
    [SerializeField] private RawImage iconUI;
    [SerializeField] private TMP_Text unlockWeapon;

    private void Start()
    {
        MyLevel.Unlock2lvl += Unlock;
    }

    public void UpdateUIPlayerClip(int PlayerClip,int CountBullets, RawImage icon)
    {
         clipUI.text  =  $"X{PlayerClip}/{CountBullets}"; 
         iconUI.texture = icon.texture;
         
    }

    private void Unlock()
    {
        unlockWeapon.gameObject.SetActive(true);
        MyLevel.Unlock2lvl -= Unlock;
    }
}
