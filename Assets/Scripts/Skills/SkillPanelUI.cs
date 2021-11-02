using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanelUI : MonoBehaviour
{
    [SerializeField] private List<Button> skillButtons;
    
    public SkillSystem PlayerSkillSystem { private get; set; }

    private void Start()
    {
        skillButtons[0].onClick.AddListener(PlayerSkillSystem.HealSkill); 
        skillButtons[1].onClick.AddListener(PlayerSkillSystem.BoostSpeedSkill);
        skillButtons[2].onClick.AddListener(PlayerSkillSystem.IncreasesDamageSkill);

        PlayerSkillSystem.IsHeal += () => Debug.Log("Heal");
        PlayerSkillSystem.IsBoostSpeed += () => Debug.Log("Boost");
        PlayerSkillSystem.IsIncreaseDamage += () => Debug.Log("Damage");
    }
    
    
}
