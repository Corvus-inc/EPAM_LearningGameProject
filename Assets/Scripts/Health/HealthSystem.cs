using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem
{
    public event EventHandler OnHealthChanged;
    public event EventHandler OnHealthStateMin;

    private int _health;
    private int _healthMax;
    private int _healthMin;

    public HealthSystem(int healthMax)
    {
        _healthMax = healthMax;
        _health = healthMax;
        _healthMin = 0;
    }

    public int GetHealth()
    {
        return _health;
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
}
