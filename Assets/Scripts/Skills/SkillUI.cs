using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image icon;
    [SerializeField] private Image iconMask;

    public Button CurrentButton => button;
    public Image Icon => icon;
    public Image IconMask => iconMask;
}
