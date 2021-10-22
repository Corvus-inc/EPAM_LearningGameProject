using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Starter : MonoBehaviour
{
    [SerializeField] private PlayerCharacter _player;
    [SerializeField] private HealthBar _playerUIHealthBar;
    [SerializeField] private TMP_Text _playerIUClip;

    private PlayerStats _playerData;
    private StatsSystem _playerStatsSystem;
    private HealthSystem _playerHealthSystem;

    private void Awake()
    {
        PlayerStats ps;
        PlayerStats CheckPlayerDataOrCreateBaseStats()
        {
            if (_playerData == null)
            {
                return  ps = new PlayerStats()
                {
                    Heals = 100,
                    Speed = 10,
                    TimeBoostSpeed = 2,
                    BoostSpeedRate = 5
                };
            }
            else return ps = _playerData;
        }
        ps = CheckPlayerDataOrCreateBaseStats();
        
        _playerStatsSystem = new StatsSystem(ps);
        _player.SetStatsSystem(_playerStatsSystem);
        
        _playerHealthSystem = new HealthSystem(_playerStatsSystem.Health);
        _player.SetHealthSystem(_playerHealthSystem);
        
        _playerUIHealthBar.Setup(_playerHealthSystem);
        _playerUIHealthBar.SetColour(new Color32(33, 6, 102, 255));
    }

    private void LoadPlayerData()
    {
        _playerData = _playerStatsSystem.LoadPlayerDataFromPlayerPrefs();
    }

    private void Update()
    {
        //move in UI
        _playerIUClip.text  =  $"X{_player.PlayerClip}";
    }
}
