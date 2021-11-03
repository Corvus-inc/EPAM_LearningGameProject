using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSystem
{
    public event Action IsHeal;
    public event Action IsBoostSpeed;
    public event Action IsIncreaseDamage;
    
    private HealthSystem _healthSystem;
    private PlayerCharacter _player;
    private Func<Weapon> _getWeapon;
    private Weapon _currentWeapon;
    
    private bool BoostON;

    public SkillSystem(HealthSystem healthSystem, PlayerCharacter player, Func<Weapon>  getWeapon)
    {
        _healthSystem = healthSystem;
        _player = player;
        _getWeapon = getWeapon;
        _currentWeapon = getWeapon.Invoke();
    }

    public void HealSkill()
    {
        _healthSystem.Heal(20);
        IsHeal.Invoke();
    }

    public void BoostSpeedSkill()
    {
        if (!BoostON)  
        { 
            BoostON = true;
            _player.IsBoostedSpeed = true;
        }
        else
        {
            BoostON = false;
            _player.IsBoostedSpeed = false;
        }
        IsBoostSpeed.Invoke();
    }

    public void IncreasesDamageSkill()
    {
        _currentWeapon = _getWeapon.Invoke();
        _currentWeapon.StartDoubleDamage(20);
        IsIncreaseDamage.Invoke();
    }
}
