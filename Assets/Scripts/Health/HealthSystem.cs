﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem
{
    public StatLoader StatLoader { private get; set; }
    
    public event EventHandler OnHealthChanged;
    public event EventHandler OnHealthStateMin;

    private int _health;
    private int _healthMax;
    private int _healthMin;

    public int MaxHeals => _healthMax;
    public int Health => _health;

    public HealthSystem(int healthMax)
    {
        _healthMax = healthMax;
        _health = healthMax;
        _healthMin = 0;
    }
    public HealthSystem(int healthMax, int currentHealth, StatLoader statLoader)
    {
        StatLoader = statLoader;
        StatLoader.OnSaveHealthPlayerData += SaveHealth;

        _healthMax = healthMax;
        _health = currentHealth;
        _healthMin = 0;
    }

    public float GetHealthPrecent()
    {
        return (float)_health / _healthMax;
    }

    public void Damage(int damageAmount)
    {
        _health -= damageAmount;

        if (_health < _healthMin)
        {
            _health = 0;
            if (OnHealthStateMin != null) OnHealthStateMin(this, EventArgs.Empty);
        }
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }

    public void Heal(int healAmount)
    {
        _health += healAmount;

        if (_health > _healthMax) _health = _healthMax;

        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }

    private void SaveHealth()
    {
        StatLoader.HealthPlayerData.Health = _health;
    }
}
