using System.Collections;
using System.Collections.Generic;
using Stats;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCharacter : MonoBehaviour
{
    public WeaponSystem WeaponSystem { private get; set; }
    public HealthSystem HealthSystem { private get; set; }
    public GameState GameState { get; set; }
    public int CountBullets{ get; set; }

    public int PlayerClip => _playerWeapon.CountBulletInTheClip;
    public int MaxHealthPlayer => HealthSystem.MaxHeals;
    public int CurrentHealthPlayer => HealthSystem.Health;
    
    [SerializeField] private LayerMask layerEnemy;
    [SerializeField] private Transform targetForLook;
    [SerializeField] private  List<GameObject> prefabsWeapon;

    private float _speed;
    private float _boostSpeedRate;
    public GameObject _gunEquipped{ get; private set; }
    private Weapon _playerWeapon;
    private List<GameObject> _listGun;
    private int CountGun => _listGun.Count;
    private int _indexGun = 0;
    // temporarily
    private int _countBulletInTheClip;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _playerWeapon.ReturnAllBulletToSpawn();
            WeaponSystem.UnequippedGun();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            InitWeapon();
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

    private void Start()
    {
        InitWeapon();
        
        GameState.IsSaveProgress += () => SavePlayerStats(CollectPlayerStats());
        HealthSystem.OnHealthStateMin += PlayerDie;
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
        bool check = CheckLayerMask(collision.gameObject, layerEnemy);
        if (check)
        {
            HealthSystem.Damage(20);
        }
    }

    private void CharacterMove()
    {
        float currentSpeed = _speed;

        var boostAxis = Input.GetAxis("BoostSpeed");
        var verticalAxis = Input.GetAxis("Vertical");
        var horiontalAxis = Input.GetAxis("Horizontal");

        if (boostAxis != 0)
        {
            currentSpeed *= _boostSpeedRate * boostAxis;
        }

        if (verticalAxis != 0)
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
    
    private void LookAtTargetForPlayer(Transform targetForLook)
    { 
        var positionsForLook = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        targetForLook.position = new Vector3(positionsForLook.x, transform.position.y, positionsForLook.y);

        transform.LookAt(targetForLook);       
    }
    
    private bool CheckLayerMask(GameObject obj, LayerMask layers) //For this method need creating service
    {
        if (((1 << obj.layer) & layers) != 0)
        {
            return true;
        }

        return false;
    }

    #region statsmethods
    public void InitializationPlayerStats(PlayerStats playerData)
    {
        _speed = playerData.speed;
        _boostSpeedRate = playerData.boostSpeedRate;
        CountBullets = playerData.countBullets;
        var position = playerData.playerPosition;
        transform.position = new Vector3(position[0], position[1], position[2]);
        // temporarily
        _countBulletInTheClip = playerData.countClip[_indexGun];
        //if have many weapon need save lastWeapon
    }
    
    private PlayerStats CollectPlayerStats()
    {
        var position = transform.position;
        var ps = new PlayerStats
        {
            speed = _speed,
            maxHealth = MaxHealthPlayer,
            health = CurrentHealthPlayer,
            countBullets = CountBullets,
            boostSpeedRate = _boostSpeedRate,
            playerPosition = new []
            {
                position.x,
                position.y,
                position.z,
            },
            // temporarily
            countClip = new []{_listGun[0].GetComponent<Weapon>().CountBulletInTheClip,_listGun[1].GetComponent<Weapon>().CountBulletInTheClip}
        };
        return ps;
    }

    private void SavePlayerStats(PlayerStats stats)
    {
        SavingSystem.Save(stats, "PlayerData");
    }
#endregion

#region equipWweapons
    public void InitWeapon()
    {
        _playerWeapon = WeaponSystem.GetEquippedWeapon();
        _playerWeapon.GameState = GameState;
    }
    private void SetShotgun(int index)
    {
        _gunEquipped = _listGun[index];
        InitWeapon();
    }   
#endregion
}
