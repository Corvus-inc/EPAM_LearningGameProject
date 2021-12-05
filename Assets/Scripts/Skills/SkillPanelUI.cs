using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillPanelUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ISkillSystem PlayerSkillSystem { private get; set; }

    [SerializeField] private List<SkillUI> skills;

    private List <Button> skillButtons;
    private ButtonMask _currentMask;
    private Dictionary<Skill, ButtonMask> masks;
    
    private void Start()
    {
        #region Init skills UI

        masks = new Dictionary<Skill, ButtonMask>();
        
        foreach (var skill in skills)
        {
            
            switch (skill.Type)
            {
                case Skill.Heal:
                    skill.Button.onClick.AddListener(PlayerSkillSystem.HealSkill);
                    masks.Add(skill.Type, skill.Mask);
                    PlayerSkillSystem.IsHeal += AddTimeForMask;
                    break;
                case Skill.BoostSpeed:
                    skill.Button.onClick.AddListener(PlayerSkillSystem.BoostSpeedSkill);
                    masks.Add(skill.Type, skill.Mask);
                    PlayerSkillSystem.IsBoostSpeed += AddTimeForMask;
                    break;
                case Skill.IncreaseDamage:
                    skill.Button.onClick.AddListener(PlayerSkillSystem.IncreasesDamageSkill);
                    masks.Add(skill.Type, skill.Mask);
                    PlayerSkillSystem.IsIncreaseDamage += AddTimeForMask;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        #endregion
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _currentMask = masks[Skill.Heal];
            PlayerSkillSystem.HealSkill();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _currentMask = masks[Skill.BoostSpeed];
            PlayerSkillSystem.BoostSpeedSkill();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            
            _currentMask = masks[Skill.IncreaseDamage];
            PlayerSkillSystem.IncreasesDamageSkill();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        WeaponHolder.ShootIsLocked = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        WeaponHolder.ShootIsLocked = false;
    }

    //for created types
    private void AddTimeForMask(float time_mSec)
    {
        _currentMask.TimeForMasked = time_mSec / 1000;
        _currentMask.gameObject.SetActive(true);
    }

}