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
    [SerializeField] private SkillPanelUI playerSkillPanelUI;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private WeaponController weaponController;
    
    [SerializeField] private TMP_Text playerIuClip;

    private HealthSystem _playerHealthSystem;
    private SkillSystem _playerSkillSystem;
    private PlayerStats _loaderData;
    private StatLoader _loader;
    
    private void Awake()
    {
        Initialize();
        SetDependencies();
    }

    private void Initialize()
    {
        _loader = new StatLoader(GameState.GameIsLoaded);
        _loaderData = _loader.LoadablePlayerStats;
        GameState.GameIsLoaded = false;
        
        player.InitializationPlayerStats(_loaderData);
        
        _playerHealthSystem = new HealthSystem(_loaderData.maxHealth, _loaderData.health);
        
        playerUIHealthBar.SetSize(_playerHealthSystem.Health);
        playerUIHealthBar.SetColour(new Color32(33, 6, 102, 255));

        _playerSkillSystem = new SkillSystem(_playerHealthSystem, player, player.PlayerWeapon);
    }

    private void SetDependencies()
    {
        player.GameState = gameState;
        player.HealthSystem = _playerHealthSystem;
        
        playerUIHealthBar.HealthSystem = _playerHealthSystem;
        
        pauseMenu.GameState = gameState;
        pauseMenu.Loader = _loader;
        
        weaponController.GameState = gameState;

        playerSkillPanelUI.PlayerSkillSystem = _playerSkillSystem;
    }

    private void Update()
    {
        //TODO: Don't check every frame, create an event that changes UI when the PlayerClip value has changed
        playerIuClip.text  =  $"X{player.PlayerClip}/{player.CountBullets}"; 
    }
}
