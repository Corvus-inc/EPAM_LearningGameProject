using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanelUI : MonoBehaviour
{
    public SkillSystem PlayerSkillSystem { private get; set; }
    
    [SerializeField] private List<Button> skillButtons;
    [SerializeField] private List<SkillUI> skills;
    
    private Image[] _iconsMask;
    private ButtonMask[] _currentMasks;

    private void Awake()
    {
        InitIcons();
        InitMasks();
    }

    private void Start()
    { 
        skillButtons[0].onClick.AddListener(PlayerSkillSystem.HealSkill); 
        skillButtons[1].onClick.AddListener(PlayerSkillSystem.BoostSpeedSkill);
        skillButtons[2].onClick.AddListener(PlayerSkillSystem.IncreasesDamageSkill);

        PlayerSkillSystem.IsHeal += () => _currentMasks[0].gameObject.SetActive(true);  
        PlayerSkillSystem.IsBoostSpeed += () => _currentMasks[1].gameObject.SetActive(true);  
        PlayerSkillSystem.IsIncreaseDamage += () => _currentMasks[2].gameObject.SetActive(true);  
       
    }

    private void InitIcons()
    {
        _iconsMask = new Image[skills.Count];
        for (var i = 0; i < _iconsMask.Length; i++)
        {
            _iconsMask[i] = skills[i].IconMask;
            _iconsMask[i].fillAmount = 0;

        }
    } 
    
    private void InitMasks()
    {
        _currentMasks = new ButtonMask[_iconsMask.Length];
        for (var i = 0; i < _currentMasks.Length; i++)
        {
            _currentMasks[i] = _iconsMask[i].gameObject.GetComponent<ButtonMask>();
            _currentMasks[i].IconMask = _iconsMask[i];
            
            _currentMasks[i].TimeForMasked = 5;
        }
    }
}
