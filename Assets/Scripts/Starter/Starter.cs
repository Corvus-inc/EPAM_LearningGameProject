using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour
{
    [SerializeField] private PlayerCharacter _player;
    [SerializeField] private HealthBar _playerUIHealthBar;

    private HealthSystem _palyerHealthSystem;

    private void Awake()
    {
        _palyerHealthSystem = new HealthSystem(_player.HealthPlayer);
        _player.SetHealthSystem(_palyerHealthSystem);
        _playerUIHealthBar.Setup(_palyerHealthSystem);
    }
}
