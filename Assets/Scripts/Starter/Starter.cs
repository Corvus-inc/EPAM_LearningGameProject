using System.Collections.Generic;
using LoaderSystem;
using UnityEngine;

public class Starter : MonoBehaviour
{
    [SerializeField] private GameState prefabGameState;
    
    
    [SerializeField] private UIPlayer playerUI;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private CameraFollow2 cameraFollow2;
    [SerializeField] private HealthBar playerUIHealthBar;
    [SerializeField] private SkillPanelUI playerSkillPanelUI;
    
    [SerializeField] private List<GameObject> _listPrefabWeapons;
    private List<WeaponHolder> _playerWeapons;
    
    private IHealthSystem _playerHealthSystem;
    private WeaponSystem _playerWeaponSystem;
    private ISkillSystem _playerSkillSystem;
    private IPlayerStats _loaderData;
    private IGameState _gameState;
    private IStatLoader _loader;
    private IPlayer _player;

    private ICameraFollow CameraFollow => cameraFollow2;
    private IHealthBar PlayerUIHealthBar => playerUIHealthBar;
    
    private void Awake()
    {
        Initialize();
        SetDependencies();
    }

    private void Initialize()
    {
        var gst = FindObjectOfType<GameState>();
        _gameState = gst ? gst : Instantiate(prefabGameState);
        
        _loader = new StatLoader(_gameState.GameIsLoaded, _gameState);
        _gameState.GameIsLoaded = false;

        #region Player init
        
        var goPlayer= Instantiate(playerPrefab,Vector3.zero, Quaternion.identity);
        goPlayer.transform.localScale = new Vector3(3.6f, 3.6f, 3.6f);
        _player = goPlayer.GetComponent<IPlayer>();

        _player.StatLoader = _loader;
        _player.LoadPlayer(_loader.PlayerData);

        _playerHealthSystem = new HealthSystem(_loader);
        
        //How Created weapons?
        var creator = new GameObject().AddComponent<WeaponCreator>().GetComponent<WeaponCreator>();
        _playerWeapons = creator.InitStoreWeapons(_listPrefabWeapons, goPlayer.transform);
        Destroy(creator.gameObject); 
        
        _playerWeaponSystem = new WeaponSystem(_playerWeapons, goPlayer.transform, playerUI, _player.CountBullets, _loader);
        
        PlayerUIHealthBar.SetSize(_playerHealthSystem.Health);
        PlayerUIHealthBar.SetColour(new Color32(33, 6, 102, 255));
        
        _playerSkillSystem = new SkillSystem(_playerHealthSystem, _player, _playerWeaponSystem);

        #endregion
        CameraFollow.FollowTarget = goPlayer.transform;
    }

    private void SetDependencies()
    {
        _player.GameState = _gameState;
        _player.HealthSystem = _playerHealthSystem;
        _player.WeaponSystem = _playerWeaponSystem;
        
        playerUIHealthBar.HealthSystem = _playerHealthSystem;
        
        pauseMenu.GameState = _gameState;
        pauseMenu.Loader = _loader;
        
        playerSkillPanelUI.PlayerSkillSystem = _playerSkillSystem;
    }
}
