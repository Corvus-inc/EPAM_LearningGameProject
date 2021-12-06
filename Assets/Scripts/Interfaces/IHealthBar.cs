using UnityEngine;

public interface IHealthBar
{
    IHealthSystem HealthSystem { set; }
    void SetSize(float health);
    void SetColour(Color color);
}