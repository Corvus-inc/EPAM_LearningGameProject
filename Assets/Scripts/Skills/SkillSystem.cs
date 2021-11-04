using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Timers;
using UnityEngine;

public class SkillSystem
{
    public event Action<float> IsHeal;
    public event Action<float> IsBoostSpeed;
    public event Action<float> IsIncreaseDamage;

    private HealthSystem _healthSystem;
    private PlayerCharacter _player;
    private Func<Weapon> _getWeapon;
    private Weapon _currentWeapon;
    private bool _timerContinues;

    private const float _mSecForHealSkill = 1000;
    private const float _mSecForBoostSkill = 2000;
    private const float _mSecForDamageSkill = 5000;

    public SkillSystem(HealthSystem healthSystem, PlayerCharacter player, Func<Weapon>  getWeapon)
    {
        _healthSystem = healthSystem;
        _player = player;
        _getWeapon = getWeapon;
        _currentWeapon = getWeapon.Invoke();
    }

    public void HealSkill()
    {
        TimerSkillManager(
            () =>
            {
                const int valueHeal = 20;
                _healthSystem.Heal(valueHeal);
            },
            () =>
            {
                Debug.Log("Heal Recharged!");
            }
            );
        IsHeal.Invoke(_mSecForHealSkill);
    }

    public void BoostSpeedSkill()
    { 
        TimerSkillManager(
            () =>
            {
                _player.IsBoostedSpeed = true;
            },
            () =>
            {
                _player.IsBoostedSpeed = false;
                Debug.Log("Speed Recharged!");
            }
            );
        IsBoostSpeed.Invoke(_mSecForBoostSkill);
    }

    public void IncreasesDamageSkill()
    {
        TimerSkillManager(
            ()=>
            {
                _currentWeapon = _getWeapon.Invoke();
                _currentWeapon.StartDoubleDamage(20);
            }, 
            () =>
            {
                Debug.Log("Damage Recharged!");
            }
            );
        IsIncreaseDamage.Invoke(_mSecForDamageSkill);
    }

    private void TimerSkillManager(Action start, Action finish)
    {
        if (_timerContinues) return;
        Timer aTimer;
        //only one timer
        _timerContinues = true;

        start.Invoke();
        // _player.IsBoostedSpeed = true;
            
        aTimer = new Timer(_mSecForBoostSkill);
        aTimer.Elapsed += TimerComplete;
        aTimer.Enabled = true;
        void TimerComplete(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            _timerContinues = false;

            finish.Invoke();
            // _player.IsBoostedSpeed = false;
            
            aTimer.Stop();
            aTimer.Dispose();
        }
    }
}
