using System;

namespace LoaderSystem
{
    public interface IStatLoader
    {
        event Action OnSavePlayerData;
        PlayerData PlayerData { get; } 
        HealthPlayerData HealthPlayerData { get; }
        WeaponPlayerData WeaponPlayerData { get; }
    }
}