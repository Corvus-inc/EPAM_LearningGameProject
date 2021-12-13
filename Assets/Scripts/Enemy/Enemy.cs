using Sounds;
using UnityEngine;

public class Enemy : BaseHaveHealth, IEnemy
{
    [Range(0,1000)] [SerializeField] private int _startHealthEnemy;
    [Range(0,10)] [SerializeField] private float _speedEnemy;

    [SerializeField] private LayerMask _layerPlayer;
    [SerializeField] private LayerMask _layerBullet;
    [SerializeField] private Animator animator;
    
    private static readonly int IsWalk = Animator.StringToHash("walk");
    private static readonly int IsRun = Animator.StringToHash("run");
    private static readonly int IsAttack = Animator.StringToHash("attack");
    
    
    private Transform _target;
    
    private void Awake()
    {
        MyHealthSystem = new HealthSystem(_startHealthEnemy);
        MyHealthSystem.OnHealthStateMin += (sender, args) => 
        { 
            Destroy(gameObject);
        };
        animator.SetTrigger(IsWalk);
    }

    void Update()
    {
        if (_target != null)
        {
            // transform.position = Vector3.MoveTowards(transform.position, _target.position, Time.deltaTime* _speedEnemy);
            transform.LookAt(_target);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
       bool check = GameUtils.Utils.CheckLayerMask(collision.gameObject, _layerBullet);
        
       if (check)
       {
           IBullet bullet = collision.gameObject.GetComponent<IBullet>();
           int damage = bullet.GetBulletDamage();
           MyHealthSystem.Damage(damage);
           SoundManager.PlaySound(Sound.EnemyHit);
            
           StopCoroutine(bullet.DeactivatingBullet(0));
           bullet.DeactivatingBullet();
       }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameUtils.Utils.CheckLayerMask(other.gameObject, _layerPlayer))
        {
            _target = other.transform;
            enabled = true;
            animator.SetTrigger(IsRun);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (GameUtils.Utils.CheckLayerMask(other.gameObject, _layerPlayer))
        {
            _target = null;
            enabled = false;
            animator.SetTrigger(IsWalk);
        }
    }

    public void DamagePlayer()
    {
        _target.gameObject.GetComponent<PlayerCharacter>().HealthSystem.Damage(20);
    }

    public void Attack(Transform targetPosition)
    {
        animator.SetTrigger(IsRun);
        animator.SetTrigger(IsAttack);
    }
}