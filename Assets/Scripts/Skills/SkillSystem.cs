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
    private Weapon _weapon;
    private bool BoostON;

    public SkillSystem(HealthSystem healthSystem, PlayerCharacter player, Weapon weapon)
    {
        _healthSystem = healthSystem;
        _player = player;
        _weapon = weapon;
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
            _player.isBoostedSpeed = true;
        }
        else
        {
            BoostON = false;
            _player.isBoostedSpeed = false;
        }
        IsBoostSpeed.Invoke();
    }

    public void IncreasesDamageSkill()
    { 
        _weapon.StartDoubleDamage(3);
        IsIncreaseDamage.Invoke();
    }
}
