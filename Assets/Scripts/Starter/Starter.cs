using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    [SerializeField] private PlayerCharacter player;
    [SerializeField] private HealthBar playerUIHealthBar;
    [SerializeField] private SkillPanelUI playerSkillPanelUI;
    [SerializeField] private UIPlayer playerUI;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private List<GameObject> _listPrefabWeapons;
    

    private WeaponSystem _playerWeaponSystem;
    private HealthSystem _playerHealthSystem;
    private SkillSystem _playerSkillSystem;
    private List<GameObject> _weapons;
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

        InitStoreWeapons();
        _playerWeaponSystem = new WeaponSystem(_weapons, player.transform, playerUI, player.CountBullets);
        
        playerUIHealthBar.SetSize(_playerHealthSystem.Health);
        playerUIHealthBar.SetColour(new Color32(33, 6, 102, 255));
        
        _playerSkillSystem = new SkillSystem(_playerHealthSystem, player, _playerWeaponSystem.GetCurrentWeapon);
    }

    private void SetDependencies()
    {
        player.GameState = gameState;
        player.HealthSystem = _playerHealthSystem;
        player.WeaponSystem = _playerWeaponSystem;
        
        playerUIHealthBar.HealthSystem = _playerHealthSystem;
        
        pauseMenu.GameState = gameState;
        pauseMenu.Loader = _loader;
        
        playerSkillPanelUI.PlayerSkillSystem = _playerSkillSystem;
    }

    private void InitStoreWeapons()
    {
        var storePosition = Instantiate(new GameObject("StoreWeapons")).transform;
        storePosition.position = Vector3.down;
        _weapons = new List<GameObject>();
        foreach (var weapon in _listPrefabWeapons)
        {
            var newWeapon = Instantiate(weapon, player.transform);
            _weapons.Add(newWeapon);
            newWeapon.transform.SetParent(storePosition);
            newWeapon.SetActive(false);
        }
    }
}
