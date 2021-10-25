using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public GameState GameState { private get; set; }
    public HealthSystem HealthSystem { private get; set; }

    [SerializeField] private Transform targetForLook;
    [SerializeField] private LayerMask _layerEnemy;
    [SerializeField] private WeaponController _playerWeapon;

    [Range(0, 1000)] [SerializeField] private int _healthPlayer;
    [Range(1, 20)] [SerializeField] private float mooveSpeed = 5f;
    [Range(1, 5)] [SerializeField] private float boostSpeedRate;
    

    public int PlayerClip => _playerWeapon.bulletCountInTheClip;
    public int HealthPlayer => _healthPlayer;

    void FixedUpdate()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Backspace))
            HealthSystem.Damage(20);
        if (Input.GetKeyDown(KeyCode.Space)) HealthSystem.Heal(20);
#endif
        CharacterMove();
        LookAtTargetforPlayer(targetForLook);
    }

    private void Start()
    {
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
        float currentSpeed = mooveSpeed;

        var boostAxis = Input.GetAxis("BoostSpeed");
        var verticalAxis = Input.GetAxis("Vertical");
        var horiontalAxis = Input.GetAxis("Horizontal");

        if (boostAxis != 0)
        {
            currentSpeed *= boostSpeedRate * boostAxis;
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

    private void LookAtTargetforPlayer(Transform targetForLook)
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
    
    public HealthSystem GetHealthSystem()
    {
        return HealthSystem;
    }

    public void SetHealthSystem(HealthSystem healthSystem)
    {
        HealthSystem = healthSystem;
    }
}
