using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private float _speed;
    private int _countBullets;
    private float _boostSpeedRate;
    
    [SerializeField] private LayerMask _layerEnemy;
    [SerializeField] private Transform targetForLook;
    [SerializeField] private WeaponController _playerWeapon;

    public StatLoader Loader{ private get; set; }
    public GameState GameState { private get; set; }
    public HealthSystem HealthSystem { private get; set; }

    public int PlayerClip => _playerWeapon.bulletCountInTheClip;
    public int MaxHealthPlayer => HealthSystem.MaxHeals;
    public int CurrentHealthPlayer => HealthSystem.Health;

    void FixedUpdate()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            HealthSystem.Damage(20);
        }
        if (Input.GetKeyDown(KeyCode.Space)) HealthSystem.Heal(20);
#endif
        CharacterMove();
        LookAtTargetForPlayer(targetForLook);
    }

    private void Start()
    {
        // instead, add event for collecting stats in loader. 
        HealthSystem.OnHealthChanged += (sender, args) => Loader.SavablePlayerStats = CollectPlayerStats();
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
        bool check = CheckLayerMask(collision.gameObject, _layerEnemy);
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
        Vector3 positionsForLook = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
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
        _countBullets = playerData.countBullets;
        transform.position = playerData.playerPosition;
    }
    
    private PlayerStats CollectPlayerStats()
    {
        var ps = new PlayerStats
        {
            speed = _speed,
            maxHealth = MaxHealthPlayer,
            health = CurrentHealthPlayer,
            countBullets = _countBullets,
            boostSpeedRate = _boostSpeedRate,
            playerPosition = transform.position
        };
        return ps;
    }
}
