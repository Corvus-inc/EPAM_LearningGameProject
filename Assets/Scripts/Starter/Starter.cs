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
    [SerializeField] private List<GameObject> _listPrefabWeapons;
    
    [SerializeField] private UIPlayer playerUI;

    private WeaponSystem _playerWeaponSystem;
    private HealthSystem _playerHealthSystem;
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

        List<GameObject> weapons = new List<GameObject>();
        foreach (var weapon in _listPrefabWeapons)
        {
            var newWeapon = Instantiate(weapon, player.transform);
            weapons.Add(newWeapon);
            newWeapon.transform.SetParent(null);
        }
        _playerWeaponSystem = new WeaponSystem(weapons, player.transform, playerUI, player.CountBullets);
        
        playerUIHealthBar.SetSize(_playerHealthSystem.Health);
        playerUIHealthBar.SetColour(new Color32(33, 6, 102, 255));
    }

    private void SetDependencies()
    {
        player.GameState = gameState;
        player.HealthSystem = _playerHealthSystem;
        player.WeaponSystem = _playerWeaponSystem;
        
        playerUIHealthBar.HealthSystem = _playerHealthSystem;
        
        pauseMenu.GameState = gameState;
        pauseMenu.Loader = _loader;
    }

}
