using Sounds;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Range(0,1000)] [SerializeField] private int _startHealthEnemy;
    [Range(0,10)] [SerializeField] private float _speedEnemy;

    [SerializeField] private LayerMask _layerPlayer;
    [SerializeField] private LayerMask _layerBullet;
    [SerializeField] private HealthBarEnemy _healthBarEnemy;

    private HealthSystem _healthSystem;
    private Transform _target;

    private void Awake()
    {
        _healthSystem = new HealthSystem(_startHealthEnemy);
        _healthSystem.OnHealthStateMin += EnemyDie;
        _healthBarEnemy.HealthSystem = _healthSystem;
        _healthBarEnemy.SetColour(Color.red);
    }

    void Update()
    {
        if (_target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, Time.deltaTime* _speedEnemy);
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
           _healthSystem.Damage(damage);
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
        }
    }
     private void EnemyDie(object sender, System.EventArgs e)
    {
        _healthSystem.OnHealthStateMin -= EnemyDie;
        Destroy(gameObject);
    }
}
