using System;
using System.Timers;
using UnityEngine;
using Object = UnityEngine.Object;

public class SkillSystem : ISkillSystem
{
    public event Action<float> IsHeal;
    public event Action<float> IsBoostSpeed;
    public event Action<float> IsIncreaseDamage;

    private readonly IPlayer _player;
    private readonly WeaponSystem _weaponSystem;
    private readonly IHealthSystem _healthSystem;
    
    private WeaponHolder _currentWeaponHolder;

    //Characteristics for skill object
    private const float MSecForHealSkill = 1000;
    private const float MSecForBoostSkill = 2000;
    private const float MSecForDamageSkill = 5000;

    private bool _continuesHealSkill;
    private bool _continuesBoostSkill;
    private bool _continuesDamageSkill;

    public SkillSystem(IHealthSystem healthSystem, IPlayer player, WeaponSystem weaponSystem)
    {
        _healthSystem = healthSystem;
        _player = player;
        _weaponSystem = weaponSystem;
        _currentWeaponHolder = _weaponSystem.GetCurrentWeaponHolder();
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
        IsHeal?.Invoke(MSecForHealSkill);
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
        IsBoostSpeed?.Invoke(MSecForBoostSkill);
    }

    public void IncreasesDamageSkill()
    {
        TimerSkillManager(
            ()=>
            {
                _currentWeaponHolder = _weaponSystem.GetCurrentWeaponHolder();
                var weapon = _currentWeaponHolder.GunCurrent;
                weapon.StartDoubleDamage(MSecForDamageSkill/1000);
            }, 
            () =>
            {
                _continuesDamageSkill = false;
                Debug.Log("Damage Recharged!");
            },
            ref _continuesDamageSkill
            );
        IsIncreaseDamage?.Invoke(MSecForDamageSkill);
    }

    private void TimerSkillManager(Action start, Action finish,ref bool timerContinues)
    {
        if (timerContinues) return;
        Timer aTimer;
        //only one timer
        timerContinues = true;

        start.Invoke();
        // _player.IsBoostedSpeed = true;
            
        aTimer = new Timer(MSecForBoostSkill);
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