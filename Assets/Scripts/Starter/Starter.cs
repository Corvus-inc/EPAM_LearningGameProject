using System.Collections.Generic;
using LoaderSystem;
using UnityEngine;

public class Starter : MonoBehaviour
{
    [SerializeField] private UIPlayer playerUI;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private GameState gameState;
    [SerializeField] private PlayerCharacter player;
    [SerializeField] private HealthBar playerUIHealthBar;
    [SerializeField] private CameraFollow2 cameraFollow2;
    [SerializeField] private SkillPanelUI playerSkillPanelUI;
    [SerializeField] private List<GameObject> _listPrefabWeapons;
    
    private IHealthSystem _playerHealthSystem;
    private WeaponSystem _playerWeaponSystem;
    private SkillSystem _playerSkillSystem;
    private List<Weapon> _playerWeapons;
    private PlayerStats _loaderData;
    private StatLoader _loader;

    private ICameraFollow CameraFollow=> cameraFollow2;
    private IHealthBar PlayerUIHealthBar => playerUIHealthBar;
    private void Awake()
    {
        Initialize();
        SetDependencies();
    }

    private void Initialize()
    {
        CameraFollow.FollowTarget = player.transform;
        
        _loader = new StatLoader(GameState.GameIsLoaded, gameState);
        GameState.GameIsLoaded = false;

        #region Player init

        player.StatLoader = _loader;
        player.LoadPlayer(_loader.LoadPlayerData());

        _playerHealthSystem = new HealthSystem(_loader);
        
        //How Created weapons?
        var creator = new GameObject().AddComponent<WeaponCreator>().GetComponent<WeaponCreator>();
        _playerWeapons = creator.InitStoreWeapons(_listPrefabWeapons, player.transform);
        Destroy(creator.gameObject); 
        
        _playerWeaponSystem = new WeaponSystem(_playerWeapons, player.transform, playerUI, player.CountBullets, _loader);
        
        PlayerUIHealthBar.SetSize(_playerHealthSystem.Health);
        PlayerUIHealthBar.SetColour(new Color32(33, 6, 102, 255));
        
         _playerSkillSystem = new SkillSystem(_playerHealthSystem, player, _playerWeaponSystem);

        #endregion
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
