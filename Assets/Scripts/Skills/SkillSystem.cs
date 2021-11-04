using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Timers;
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
    
    private float _mSecForRun = 2000;
    private bool _timerContinues;

    public SkillSystem(HealthSystem healthSystem, PlayerCharacter player, Func<Weapon>  getWeapon)
    {
        _healthSystem = healthSystem;
        _player = player;
        _getWeapon = getWeapon;
        _currentWeapon = getWeapon.Invoke();
    }

    public void HealSkill()
    {
        const int valueHeal = 20;
        _healthSystem.Heal(valueHeal);
        
        IsHeal.Invoke();
    }

    public void BoostSpeedSkill()
    { 
        TimerSkillManager(
            () =>{_player.IsBoostedSpeed = true;},
            ()=> {_player.IsBoostedSpeed = false;}
            );
        IsBoostSpeed.Invoke();
    }

    public void IncreasesDamageSkill()
    {
        _currentWeapon = _getWeapon.Invoke();
        _currentWeapon.StartDoubleDamage(20);
        IsIncreaseDamage.Invoke();
    }

    private void TimerSkillManager(Action start, Action finish)
    {
        if (!_timerContinues)
        {
            Timer aTimer;
            //only one timer
            _timerContinues = true;

            start.Invoke();
            // _player.IsBoostedSpeed = true;
            
            aTimer = new Timer(_mSecForRun);
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
}
