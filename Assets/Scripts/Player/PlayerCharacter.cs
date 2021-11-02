using System.Collections;
using System.Collections.Generic;
using Stats;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCharacter : MonoBehaviour
{
    public HealthSystem HealthSystem { private get; set; }
    public GameState GameState { private get; set; }
    public UIPlayer UI { private get; set; }
    public int CountBullets{ get; private set; }

    public int PlayerClip => _playerWeapon.CountBulletInTheClip;
    public int MaxHealthPlayer => HealthSystem.MaxHeals;
    public int CurrentHealthPlayer => HealthSystem.Health;
    
    [SerializeField] private LayerMask layerEnemy;
    [SerializeField] private Transform targetForLook;
    [SerializeField] private  List<GameObject> prefabsWeapon;

    private float _speed;
    private float _boostSpeedRate;
    private GameObject _gunEquipped;
    private WeaponController _playerWeapon;
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
            _indexGun++;
            if (_indexGun >= CountGun)
            {
                _indexGun = 0;
                SetShotgun(_indexGun);
            }
            else
            {
                SetShotgun(_indexGun);
            }
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
        #region initListGun
        _listGun = new List<GameObject>();
        foreach (var gun in prefabsWeapon)
        {
            InitWeapon(gun, true);
            TakeOffWeapon();   
        }
        SetShotgun(_indexGun);
        #endregion
        GameState.IsSaveProgress += () => SavePlayerStats(CollectPlayerStats());
        HealthSystem.OnHealthStateMin += PlayerDie;
        //temporarily
        _playerWeapon.CountBulletInTheClip = _countBulletInTheClip;
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

    private void PlayerClipChanged()
    {
        
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
        _countBulletInTheClip = playerData.countClip;
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
            countClip = _playerWeapon.CountBulletInTheClip
        };
        return ps;
    }

    private void SavePlayerStats(PlayerStats stats)
    {
        SavingSystem.Save(stats, "PlayerData");
    }
#endregion

#region equipWweapons
    private void InitWeapon(GameObject weapon, bool isNew)
    {
        if (isNew)
        {
            var newWeapon = CreateNewWeapon(weapon);
            _listGun.Add(newWeapon);
            _gunEquipped = newWeapon;
        
        }
        else
        {
            _gunEquipped = weapon;
            
            _gunEquipped.SetActive(true);
            _gunEquipped.transform.SetParent(transform);
            _gunEquipped.transform.position = transform.position;
            _gunEquipped.transform.rotation = transform.rotation;
        }
        
        _playerWeapon = _gunEquipped.GetComponent<WeaponController>();
        _playerWeapon.GameState = GameState;
        _playerWeapon.IsEmptyClip += RechargeGun;
        _playerWeapon.IsChangedClip += () => {UI.UpdateUIPlayerClip(PlayerClip,CountBullets);};
        UI.UpdateUIPlayerClip(PlayerClip,CountBullets);
    }
    private void TakeOffWeapon()
    {
        _playerWeapon.IsEmptyClip -= RechargeGun;
        _playerWeapon.IsChangedClip -= () => {UI.UpdateUIPlayerClip(PlayerClip,CountBullets);};
        _gunEquipped.transform.SetParent(null);
        _gunEquipped.SetActive(false);
    }

    private void RechargeGun()
    {
        _playerWeapon.Recharge(CountBullets);
        CountBullets -= _playerWeapon.MaxBulletInTheClip;
        if (CountBullets < 0) CountBullets = 0;
    }
    private GameObject CreateNewWeapon(GameObject newWeapon)
    {
        return Instantiate(newWeapon, transform);
    }

    private void SetShotgun(int index)
    {
        if(_gunEquipped) TakeOffWeapon();
        _gunEquipped = _listGun[index];
        InitWeapon(_gunEquipped, false); 
    }   
#endregion
}
