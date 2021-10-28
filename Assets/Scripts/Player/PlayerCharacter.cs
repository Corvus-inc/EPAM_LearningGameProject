using System.Collections;
using System.Collections.Generic;
using Stats;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCharacter : MonoBehaviour
{
    public GameState GameState { private get; set; }
    public HealthSystem HealthSystem { private get; set; }
    public int CountBullets{ get; private set; }

    public int PlayerClip => playerWeapon.CountBulletInTheClip;
    public int MaxHealthPlayer => HealthSystem.MaxHeals;
    public int CurrentHealthPlayer => HealthSystem.Health;
    
    [SerializeField] private LayerMask layerEnemy;
    [SerializeField] private Transform targetForLook;
    [SerializeField] private WeaponController playerWeapon;

    private float _speed;
    private float _boostSpeedRate;

    void Update()
    {
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
        playerWeapon.IsEmptyClip += () =>
        {
            // new method
            playerWeapon.Recharge(CountBullets);
            CountBullets -= playerWeapon.MaxBulletInTheClip;
            if (CountBullets < 0) CountBullets = 0;
        };
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
    
    public void InitializationPlayerStats(PlayerStats playerData)
    {
        _speed = playerData.speed;
        _boostSpeedRate = playerData.boostSpeedRate;
        CountBullets = playerData.countBullets;
        var position = playerData.playerPosition;
        transform.position = new Vector3(position[0], position[1], position[2]);
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
            } 
        };
        return ps;
    }

    private void SavePlayerStats(PlayerStats stats)
    {
        SavingSystem.Save(stats, "PlayerData");
    }
}
