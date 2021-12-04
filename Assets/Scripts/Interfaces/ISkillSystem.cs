using System;

public interface ISkillSystem
{
    event Action<float> IsHeal;
    event Action<float> IsBoostSpeed;
    event Action<float> IsIncreaseDamage;
    
    void HealSkill();
    void BoostSpeedSkill();
    void IncreasesDamageSkill();
}