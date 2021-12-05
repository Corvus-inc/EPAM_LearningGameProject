using System;
using System.Linq;

namespace LoaderSystem
{
    public class StatLoader : IStatLoader
    {
        public PlayerData PlayerData { get; private set; }
        public HealthPlayerData HealthPlayerData { get; private set; }
        public WeaponPlayerData WeaponPlayerData { get; private set; }

        public event Action OnSavePlayerData;

        private readonly PlayerStats _startedPlayerStats = new PlayerStats()
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

        private IPlayerStats LoadablePlayerStats { get; }
        private IPlayerStats SavingPlayerStats { get; set; }

        public StatLoader(bool isLoader, IGameState gameState)
        {
            var startedData = new PlayerStats(_startedPlayerStats);
            LoadablePlayerStats = !isLoader ? startedData : SavingSystem.Load(SaveName.PlayerData, startedData);
            //where delete subscribe
            gameState.IsSaveProgress += SavePlayerStats;

            HealthPlayerData = LoadHealthPlayerData();
            WeaponPlayerData = LoadWeaponPlayerData();
        }

        public PlayerData LoadPlayerData()
        {
            PlayerData = new PlayerData
            {
                Speed = LoadablePlayerStats.Speed,
                BoostSpeedRate = LoadablePlayerStats.BoostSpeedRate,
                CountBullet = LoadablePlayerStats.CountBullets,
                Position = LoadablePlayerStats.PlayerPosition
            };
            return PlayerData;
        }

        private void SavePlayerStats()
        {
            OnSavePlayerData?.Invoke();

            var savingPlayerData = new PlayerStats
            {
                Health = HealthPlayerData.Health,
                MAXHealth = _startedPlayerStats.Health,
                Speed = PlayerData.Speed,
                BoostSpeedRate = _startedPlayerStats.BoostSpeedRate,
                CountBullets = PlayerData.CountBullet,
                PlayerPosition = PlayerData.Position,
                StartedWeapon = WeaponPlayerData.Index,
                WeaponSavingStatsArray = WeaponPlayerData.WeaponSavingStatsList.ToArray()
            };

            SavingPlayerStats = savingPlayerData;
            SavingSystem.Save(SavingPlayerStats, SaveName.PlayerData);
        }


        private HealthPlayerData LoadHealthPlayerData()
        {
            HealthPlayerData = new HealthPlayerData
            {
                MaxHealth = LoadablePlayerStats.MAXHealth,
                Health = LoadablePlayerStats.Health
            };
            return HealthPlayerData;
        }

        private WeaponPlayerData LoadWeaponPlayerData()
        {
            WeaponPlayerData = new WeaponPlayerData
            {
                Index = LoadablePlayerStats.StartedWeapon,
                WeaponSavingStatsList = LoadablePlayerStats.WeaponSavingStatsArray.ToList()
            };
            return WeaponPlayerData;
        }
    }
}