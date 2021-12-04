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

    private void Start()
    {
        #region Init skills UI
        
        foreach (var skill in skills)
        {
            _currentMask = skill.IconMask.gameObject.GetComponent<ButtonMask>();
            
            switch (skill.Type)
            {
                case Skill.Heal:
                    skill.Button.onClick.AddListener(PlayerSkillSystem.HealSkill);
                    PlayerSkillSystem.IsHeal += AddTimeForMask;
                    break;
                case Skill.BoostSpeed:
                    skill.Button.onClick.AddListener(PlayerSkillSystem.BoostSpeedSkill);
                    PlayerSkillSystem.IsHeal += AddTimeForMask;
                    break;
                case Skill.IncreaseDamage:
                    skill.Button.onClick.AddListener(PlayerSkillSystem.IncreasesDamageSkill);
                    PlayerSkillSystem.IsHeal += AddTimeForMask;
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
            PlayerSkillSystem.HealSkill();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerSkillSystem.BoostSpeedSkill();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayerSkillSystem.IncreasesDamageSkill();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Weapon.ShootIsLocked = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Weapon.ShootIsLocked = false;
    }

    //for created types
    private void AddTimeForMask(float time_mSec)
    {
        _currentMask.TimeForMasked = time_mSec / 1000;
        _currentMask.gameObject.SetActive(true);
    }

}