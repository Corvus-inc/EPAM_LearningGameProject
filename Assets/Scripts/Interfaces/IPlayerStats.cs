using System.Collections.Generic;
using LoaderSystem;

public interface IPlayerStats
{
    int Health { get; }
    float Speed { get; }
    int MAXHealth { get; }
    int CountBullets { get; }
    int StartedWeapon { get;  }
    float BoostSpeedRate { get; }
    float[] PlayerPosition { get; }
    WeaponSavingStats[] WeaponSavingStatsArray { get; }
}