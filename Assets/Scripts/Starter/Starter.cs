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
    private List<Weapon> _playerWeapons;
    private PlayerStats _loaderData;
    private StatLoader _loader;
    
    private void Awake()
    {
        Initialize();
        SetDependencies();
    }

    private void Initialize()
    {
        //1 The loader loading start data or loadable data do it in beginning the Initialize.
        _loader = new StatLoader(GameState.GameIsLoaded, gameState);
        _loaderData = _loader.LoadablePlayerStats;
        GameState.GameIsLoaded = false;
        
        player.StatLoader = _loader;
        player.LoadPlayer(_loader.LoadPlayerData());

        _playerHealthSystem = new HealthSystem(_loader);
        
        //How Created weapons?
        var creator = new GameObject().AddComponent<WeaponCreator>().GetComponent<WeaponCreator>();
        _playerWeapons = creator.InitStoreWeapons(_listPrefabWeapons, player.transform);
        Destroy(creator.gameObject); 
        
        _playerWeaponSystem = new WeaponSystem(_playerWeapons, player.transform, playerUI, player.CountBullets, _loaderData.startedWeapon);
        
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
}
