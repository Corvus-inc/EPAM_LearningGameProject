using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Starter : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    [SerializeField] private PlayerCharacter player;
    [SerializeField] private HealthBar playerUIHealthBar;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private WeaponController weaponController;
    
    [SerializeField] private TMP_Text playerIuClip;

    private HealthSystem _playerHealthSystem;

    private void Awake()
    {
        Initialize();
        SetDependencies();
    }

    private void Initialize()
    {
        _playerHealthSystem = new HealthSystem(player.HealthPlayer);
        playerUIHealthBar.SetColour(new Color32(33, 6, 102, 255));
    }

    private void SetDependencies()
    {
        player.GameState = gameState;
        player.HealthSystem = _playerHealthSystem;
        playerUIHealthBar.HealthSystem = _playerHealthSystem;
        pauseMenu.GameState = gameState;
        weaponController.GameState = gameState;
    }

    private void Update()
    {
        //TODO: Don't check every frame, create an event that changes UI when the PlayerClip value has changed
        playerIuClip.text  =  $"X{player.PlayerClip}"; 
    }
}
