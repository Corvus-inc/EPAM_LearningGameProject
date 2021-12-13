using System.Collections.Generic;
using LoaderSystem;
using Sounds;
using UnityEditor.VersionControl;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour, IPlayer
{
    public PlayerLevel MyLevel { get; set; } 
    public IStatLoader StatLoader { private get; set; }
    public WeaponSystem WeaponSystem { private get; set; }
    public IHealthSystem HealthSystem { get; set; }
    public bool IsBoostedSpeed{ private get; set; }
    ///temporary? for working with skills
    public WeaponHolder PlayerWeaponHolder { get; private set; }
    public IGameState GameState { private get; set; }
    public int CountBullets{ get; private set; }

    [SerializeField] private LayerMask layerEnemy;
    [SerializeField] private Transform targetForLook;

    private float _speed;
    private float _boostSpeedRate;
    private bool _isMoving;
    private bool UnlockSwap;

    private void Start()
    {
        PlayerWeaponHolder = WeaponSystem.GetEquippedWeapon();
        //on destroy
        StatLoader.OnSavePlayerData += SavePlayerData;
        HealthSystem.OnHealthStateMin += PlayerDie;
        MyLevel.Unlock2lvl += () => { UnlockSwap = true; };
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !GameState.GameIsPaused)
        {
            PlayerWeaponHolder.GunCurrent.UsageWeapon();
            MyLevel.Experience = 10f;

        }
        
        
        if (Input.GetKeyDown(KeyCode.Q)&&UnlockSwap)
        {
            PlayerWeaponHolder = WeaponSystem.SwitchWeapon();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayerWeaponHolder = WeaponSystem.GetEquippedWeapon();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            WeaponSystem.RechargeGun();
        }
        
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            HealthSystem.Damage(20);
        }
        if (Input.GetKeyDown(KeyCode.Space)) HealthSystem.Heal(20);
#endif
        // Is there something better for the player to pause too?
        if (GameState.GameIsPaused) return;
        CharacterMove();
        LookAtTargetForPlayer(targetForLook);
    }

    public void LoadPlayer(PlayerData loadPlayerData)
    {
        _speed = loadPlayerData.Speed;
        _boostSpeedRate = loadPlayerData.BoostSpeedRate;
        CountBullets = loadPlayerData.CountBullet;
        var position = loadPlayerData.Position;
        transform.position = new Vector3(position[0], position[1], position[2]);
    }

    private void SavePlayerData()
    {
        StatLoader.PlayerData.Speed = _speed;
        var position = transform.position;
        StatLoader.PlayerData.Position =  new []
        {
            position.x,
            position.y,
            position.z,
        };
        //move to the weapon system or create inventory or create both
        StatLoader.PlayerData.CountBullet = WeaponSystem.UserCountBullets;
    }
    
    private void PlayerDie(object sender, System.EventArgs e)
    {
        HealthSystem.OnHealthStateMin -= PlayerDie;
        gameObject.SetActive(false);
        Invoke(nameof(MessageWhenDie),0);
        GameState.EndGame();
    }

    private void MessageWhenDie()
    {
        Debug.Log("PlayerDie. Restart.");
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        bool check = GameUtils.Utils.CheckLayerMask(collision.gameObject, layerEnemy);
        if (check)
        {
            HealthSystem.Damage(20);
            SoundManager.PlaySound(Sound.PlayerHit);
        }
    }

    private void CharacterMove()
    {
        float currentSpeed = _speed;

        var boostAxis = Input.GetAxis("BoostSpeed");
        var verticalAxis = Input.GetAxis("Vertical");
        var horiontalAxis = Input.GetAxis("Horizontal");

        if (boostAxis != 0 || IsBoostedSpeed)
        {
            var axisMediator = boostAxis;
            if (boostAxis == 0) axisMediator = 1;
            currentSpeed *= _boostSpeedRate * axisMediator;
        }

        if (verticalAxis != 0 || horiontalAxis != 0)
        {
            _isMoving = true;
            SoundManager.PlaySound(Sound.PlayerMove);
            
            if (verticalAxis != 0 ) 
            {
                Vector3 toTranslate = Vector3.forward * verticalAxis * Time.deltaTime * currentSpeed;
                transform.Translate(toTranslate, Space.World);
            }

            if (horiontalAxis != 0)
            {
                Vector3 toTranslate = Vector3.right * horiontalAxis * Time.deltaTime * currentSpeed;
                transform.Translate(toTranslate, Space.World);
            }
        }
        else _isMoving = false;

    }
    
    private void LookAtTargetForPlayer(Transform targetForLook)
    { 
        var positionsForLook = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        targetForLook.position = new Vector3(positionsForLook.x, transform.position.y, positionsForLook.y);

        transform.LookAt(targetForLook);       
    }
}