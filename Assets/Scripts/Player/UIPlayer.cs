using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class UIPlayer : MonoBehaviour
{
    [SerializeField] private TMP_Text clipUI;
    public void UpdateUIPlayerClip(int PlayerClip,int CountBullets)
    {
         clipUI.text  =  $"X{PlayerClip}/{CountBullets}"; 
    }
}
