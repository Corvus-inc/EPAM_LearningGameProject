using System;
using System.Linq;

namespace LoaderSystem
{
    public class StatLoader
    {
        public PlayerData PlayerData { get; private set; }
        public HealthPlayerData HealthPlayerData { get; private set; }
        public WeaponPlayerData WeaponPlayerData { get; private set; }

        public event Action OnSavePlayerData;


        private readonly PlayerStats _started = new PlayerStats()
        {
            Health = 100,
            MAXHealth = 100,
            Speed = 20,
            BoostSpeedRate = 2,
            CountBullets = 100,
            PlayerPosition = new float[3] {0, 0, 0},
            StartedWeapon = 0,
            WeaponSavingStatsArray = new[]
            {
                new WeaponSavingStats() {ID = 0, ClipCount = 4},
                new WeaponSavingStats() {ID = 1, ClipCount = 5}
            }
        };

        public PlayerStats LoadablePlayerStats { get; private set; }
        private PlayerStats SavingPlayerStats { get; set; }

        //How transfer only delegate - gameState.IsSaveProgress
        public StatLoader(bool isLoader, GameState gameState)
        {
            var startedData = new PlayerStats(_started);
            LoadablePlayerStats = !isLoader ? startedData : SavingSystem.Load("PlayerData", startedData);
            gameState.IsSaveProgress += SavePlayerStats;

            HealthPlayerData = LoadHealthPlayerData();
            WeaponPlayerData = LoadWeaponPlayerData();
        }

        public PlayerData LoadPlayerData()
        {
            PlayerData = new PlayerData();
            PlayerData.Speed = LoadablePlayerStats.Speed;
            PlayerData.BoostSpeedRate = LoadablePlayerStats.BoostSpeedRate;
            PlayerData.CountBullet = LoadablePlayerStats.CountBullets;
            PlayerData.Position = LoadablePlayerStats.PlayerPosition;
            return PlayerData;
        }

        private void SavePlayerStats()
        {
            OnSavePlayerData?.Invoke();

            var savingPlayerData = new PlayerStats();
            savingPlayerData.Health = HealthPlayerData.Health;
            savingPlayerData.MAXHealth = _started.Health;
            savingPlayerData.Speed = PlayerData.Speed;
            savingPlayerData.BoostSpeedRate = _started.BoostSpeedRate;
            savingPlayerData.CountBullets = PlayerData.CountBullet;
            savingPlayerData.PlayerPosition = PlayerData.Position;
            savingPlayerData.StartedWeapon = WeaponPlayerData.Index;
            savingPlayerData.WeaponSavingStatsArray = WeaponPlayerData.WeaponSavingStatsList.ToArray();

            SavingPlayerStats = savingPlayerData;
            SavingSystem.Save(SavingPlayerStats, "PlayerData");
        }


        private HealthPlayerData LoadHealthPlayerData()
        {
            HealthPlayerData = new HealthPlayerData();
            HealthPlayerData.MaxHealth = LoadablePlayerStats.MAXHealth;
            HealthPlayerData.Health = LoadablePlayerStats.Health;
            return HealthPlayerData;
        }

        private WeaponPlayerData LoadWeaponPlayerData()
        {
            WeaponPlayerData = new WeaponPlayerData();
            WeaponPlayerData.Index = LoadablePlayerStats.StartedWeapon;
            WeaponPlayerData.WeaponSavingStatsList = LoadablePlayerStats.WeaponSavingStatsArray.ToList();
            return WeaponPlayerData;
        }
    }
}