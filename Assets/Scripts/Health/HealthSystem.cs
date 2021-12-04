using System;
using System.Collections;
using System.Collections.Generic;
using LoaderSystem;
using UnityEngine;

public class HealthSystem : IHealthSystem
{
    public StatLoader StatLoader { private get; set; }
    
    public event EventHandler OnHealthChanged;
    public event EventHandler OnHealthStateMin;

    private int _healthMin;

    public int MaxHeals { get; }

    public int Health { get; private set; }

    public HealthSystem(int healthMax)
    {
        MaxHeals = healthMax;
        Health = healthMax;
        _healthMin = 0;
    }
    public HealthSystem(StatLoader statLoader)
    {
        StatLoader = statLoader;
        //When player Die the subscription can multiply- on destroy
        StatLoader.OnSavePlayerData += SaveHealth;

        MaxHeals = StatLoader.HealthPlayerData.MaxHealth;
        Health = StatLoader.HealthPlayerData.Health;
        _healthMin = 0;
    }

    public float GetHealthPercent()
    {
        return (float)Health / MaxHeals;
    }

    public void Damage(int damageAmount)
    {
        Health -= damageAmount;

        if (Health < _healthMin)
        {
            Health = 0;
            if (OnHealthStateMin != null) OnHealthStateMin(this, EventArgs.Empty);
        }
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }

    public void Heal(int healAmount)
    {
        Health += healAmount;

        if (Health > MaxHeals) Health = MaxHeals;

        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }

    private void SaveHealth()
    {
        StatLoader.HealthPlayerData.Health = Health;
    }
}