using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Starter : MonoBehaviour
{
    [SerializeField] private PlayerCharacter _player;
    [SerializeField] private HealthBar _playerUIHealthBar;
    [SerializeField] private TMP_Text _playerIUClip;

    private StatsSystem _playerStatsSystem;
    private HealthSystem _palyerHealthSystem;

    private void Awake()
    {
        _playerStatsSystem = new StatsSystem(_player.HealthPlayer, 0, _player.MooveSpeed, _player.TimeBoostSpeed);
        _player.SetStatsSystem(_playerStatsSystem);
        
        _palyerHealthSystem = new HealthSystem(_playerStatsSystem.Health);
        _player.SetHealthSystem(_palyerHealthSystem);
        
        _playerUIHealthBar.Setup(_palyerHealthSystem);
        _playerUIHealthBar.SetColour(new Color32(33, 6, 102, 255));
    }

    private void Update()
    {
        //move in UI
        _playerIUClip.text  =  $"X{_player.PlayerClip}";
    }
}
