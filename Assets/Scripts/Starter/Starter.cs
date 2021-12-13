﻿using System.Collections.Generic;
using LoaderSystem;
using Sounds;
using UnityEngine;
using UnityEngine.Serialization;

public class Starter : MonoBehaviour
{
    [SerializeField] private GameState prefabGameState;
    [SerializeField] private PlayerLevel playerLevel;
    
    [SerializeField] private UIPlayer playerUI;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private WorldOverlay worldOverlay;
    [SerializeField] private SpawnerCircle zombieSpawner;
    [SerializeField] private CameraFollow2 cameraFollow2;
    [SerializeField] private HealthBar playerUIHealthBar;
    [SerializeField] private SkillPanelUI playerSkillPanelUI;
    [SerializeField] private List<Vector3> pointsForZombieSpawners;
    
    [FormerlySerializedAs("_listPrefabWeapons")] [SerializeField] private List<GameObject> listPrefabWeapons;
    
    private List<ISpawningWithHealth> _spawners;
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

        var goPlayer = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        goPlayer.transform.localScale = new Vector3(3.6f, 3.6f, 3.6f);
        _player = goPlayer.GetComponent<IPlayer>();

        _player.StatLoader = _loader;
        _player.LoadPlayer(_loader.PlayerData);

        _playerHealthSystem = new HealthSystem(_loader);

        //How Created weapons?
        var creator = new GameObject().AddComponent<WeaponCreator>().GetComponent<WeaponCreator>();
        _playerWeapons = creator.InitStoreWeapons(listPrefabWeapons, goPlayer.transform);
        Destroy(creator.gameObject);

        _playerWeaponSystem =
            new WeaponSystem(_playerWeapons, goPlayer.transform, playerUI, _player.CountBullets, _loader);

        PlayerUIHealthBar.SetSize(_playerHealthSystem.Health);
        PlayerUIHealthBar.SetColour(new Color32(33, 6, 102, 255));

        _playerSkillSystem = new SkillSystem(_playerHealthSystem, _player, _playerWeaponSystem);

        
        #endregion

        CameraFollow.FollowTarget = goPlayer.transform;
        SoundManager.Initialize();
        SoundManager.PlayBackgroundMusic(Sound.Background);

        #region zombie
        _spawners = new List<ISpawningWithHealth>();
        foreach (var point in pointsForZombieSpawners)
        {
            _spawners.Add(Instantiate(zombieSpawner, point, Quaternion.identity));
        }
        worldOverlay.ListBars = new Dictionary<HealthBarEnemy, Transform>();
        foreach (var el in _spawners)
        {
            el.spawnedObject += worldOverlay.CreateZombieHealthBar;
        }
        #endregion
    }

    private void SetDependencies()
        {
            _player.GameState = _gameState;
            _player.HealthSystem = _playerHealthSystem;
            _player.WeaponSystem = _playerWeaponSystem;
            _player.MyLevel = playerLevel;
            
            playerUI.MyLevel = playerLevel;
            playerUIHealthBar.HealthSystem = _playerHealthSystem;
        
            pauseMenu.GameState = _gameState;
            pauseMenu.Loader = _loader;
        
            playerSkillPanelUI.PlayerSkillSystem = _playerSkillSystem;
            worldOverlay.ZombieSpawnings = _spawners;
        }
}
