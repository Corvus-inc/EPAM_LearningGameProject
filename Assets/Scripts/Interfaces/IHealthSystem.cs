using System;

public interface IHealthSystem
{
    event EventHandler OnHealthChanged;
    event EventHandler OnHealthStateMin;
    int Health { get; }
    int MaxHeals { get; }
    float GetHealthPercent();
    void Damage(int damage);
    void Heal(int valueHeal);
}