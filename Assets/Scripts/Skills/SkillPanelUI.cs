using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillPanelUI : MonoBehaviour, IPointerEnterHandler , IPointerExitHandler
{
    public SkillSystem PlayerSkillSystem { private get; set; }

    [SerializeField] private List<Button> skillButtons;
    [SerializeField] private List<SkillUI> skills;
    
    // private Image[] _iconsMask;
    private ButtonMask[] _currentMasks;

    private void Awake()
    {
        // InitIcons();
        GettingMasks();
    }

    private void Start()
    { 
        skillButtons[0].onClick.AddListener(PlayerSkillSystem.HealSkill); 
        skillButtons[1].onClick.AddListener(PlayerSkillSystem.BoostSpeedSkill);
        skillButtons[2].onClick.AddListener(PlayerSkillSystem.IncreasesDamageSkill);

        PlayerSkillSystem.IsHeal += AddTimeForMask0;
        PlayerSkillSystem.IsBoostSpeed += AddTimeForMask1;
        PlayerSkillSystem.IsIncreaseDamage += AddTimeForMask2;
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
        Debug.Log("Enter");
        WeaponHolder.ShootIsLocked = true;
        
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit");
        WeaponHolder.ShootIsLocked = false;
    }

    //for created types
    private void AddTimeForMask0(float time_mSec)
    {
        _currentMasks[0].TimeForMasked = time_mSec/1000;
        _currentMasks[0].gameObject.SetActive(true);
    }
    private void AddTimeForMask1(float time_mSec)
    {
        _currentMasks[1].TimeForMasked = time_mSec/1000;
        _currentMasks[1].gameObject.SetActive(true);
    }
    private void AddTimeForMask2(float time_mSec)
    {
        _currentMasks[2].TimeForMasked = time_mSec/1000;
        _currentMasks[2].gameObject.SetActive(true);
    }
    
    // private void InitIcons()
    // {
    //     _iconsMask = new Image[skills.Count];
    //     for (var i = 0; i < _iconsMask.Length; i++)
    //     {
    //         _iconsMask[i] = skills[i].IconMask;
    //         _iconsMask[i].fillAmount = 0;
    //
    //     }
    // } 
    
    private void GettingMasks()
    {
        _currentMasks = new ButtonMask[skills.Count];
        for (var i = 0; i < _currentMasks.Length; i++)
        {
            _currentMasks[i] = skills[i].IconMask.gameObject.GetComponent<ButtonMask>();
            // _currentMasks[i].IconMask = _iconsMask[i];
            
            // _currentMasks[i].TimeForMasked = 5;
        }
    }
}
