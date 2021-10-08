using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Starter : MonoBehaviour
{
    [SerializeField] private PlayerCharacter _player;
    [SerializeField] private HealthBar _playerUIHealthBar;
    [SerializeField] private TMP_Text _playerIUClip;

    private HealthSystem _palyerHealthSystem;

    private void Awake()
    {
        _palyerHealthSystem = new HealthSystem(_player.HealthPlayer);
        _player.SetHealthSystem(_palyerHealthSystem);
        _playerUIHealthBar.Setup(_palyerHealthSystem);
        _playerUIHealthBar.SetColour(new Color32(33, 6, 102, 255));
    }

    private void Update()
    {
        
        _playerIUClip.text  =  $"X{_player.PlayerClip}";
    }
}
