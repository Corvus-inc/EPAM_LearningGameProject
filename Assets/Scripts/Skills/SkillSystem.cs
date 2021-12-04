using System;
using System.Timers;
using UnityEngine;

public class SkillSystem
{
    public event Action<float> IsHeal;
    public event Action<float> IsBoostSpeed;
    public event Action<float> IsIncreaseDamage;

    
    private IHealthSystem _healthSystem;
    private PlayerCharacter _player;
    private WeaponSystem _weaponSystem;
    private Weapon _currentWeapon;

    //Characteristics for skill object
    private const float _mSecForHealSkill = 1000;
    private const float _mSecForBoostSkill = 2000;
    private const float _mSecForDamageSkill = 5000;

    private bool _continuesHealSkill;
    private bool _continuesBoostSkill;
    private bool _continuesDamageSkill;

    public SkillSystem(IHealthSystem healthSystem, PlayerCharacter player, WeaponSystem weaponSystem)
    {
        _healthSystem = healthSystem;
        _player = player;
        _weaponSystem = weaponSystem;
        _currentWeapon = _weaponSystem.GetCurrentWeapon();
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
                _continuesHealSkill = false;
                Debug.Log("Heal Recharged!");
            },
            ref _continuesHealSkill
            );
        IsHeal?.Invoke(_mSecForHealSkill);
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
                _continuesBoostSkill = false;
                Debug.Log("Speed Recharged!");
            },
            ref _continuesBoostSkill
            );
        IsBoostSpeed?.Invoke(_mSecForBoostSkill);
    }

    public void IncreasesDamageSkill()
    {
        TimerSkillManager(
            ()=>
            {
                _currentWeapon = _weaponSystem.GetCurrentWeapon();
                _currentWeapon.StartDoubleDamage(20);
            }, 
            () =>
            {
                _continuesDamageSkill = false;
                Debug.Log("Damage Recharged!");
            },
            ref _continuesDamageSkill
            );
        IsIncreaseDamage?.Invoke(_mSecForDamageSkill);
    }

    private void TimerSkillManager(Action start, Action finish,ref bool timerContinues)
    {
        if (timerContinues) return;
        Timer aTimer;
        //only one timer
        timerContinues = true;

        start.Invoke();
        // _player.IsBoostedSpeed = true;
            
        aTimer = new Timer(_mSecForBoostSkill);
        aTimer.Elapsed += TimerComplete;
        aTimer.Enabled = true;
        void TimerComplete(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            finish.Invoke();
            
            aTimer.Stop();
            aTimer.Dispose();
        }
    }
}
