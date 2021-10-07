using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Range(0,1000)] [SerializeField] private int _startHealthEnemy;
    [Range(0,10)] [SerializeField] private float _speedEnemy;

    [SerializeField] private LayerMask _layerPlayer;
    [SerializeField] private LayerMask _layerBullet;
    [SerializeField] private HealthBarEnemy _healthBarEnemy;

    private HealthSystem _healthSystem;
    private SphereCollider _enemyTrigger;
    private Transform _target;

    void Awake()
    {
        _enemyTrigger = GetComponent<SphereCollider>();
    }

    private void Start()
    {
        _healthSystem = new HealthSystem(_startHealthEnemy);
        _healthSystem.OnHealthStateMin += EnemyDie;
        _healthBarEnemy.Setup(_healthSystem);
        _healthBarEnemy.SetColour(Color.red);//why error in Awake, but working?
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
       bool check = CheckLayerMask(collision.gameObject, _layerBullet);
        
       if (check)
        {
            IBullet bullet = collision.gameObject.GetComponent<IBullet>();
            int damage = bullet.GetBulletDamage();
            _healthSystem.Damage(damage);
            StopCoroutine(bullet.DeactivatingBullet(0));
            StartCoroutine(bullet.DeactivatingBullet(0));
        }    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CheckLayerMask(other.gameObject, _layerPlayer))
        {
            _target = other.transform;
            //enemyTrigger.enabled = false;
            enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
    }

    private bool CheckLayerMask(GameObject obj, LayerMask layers)
    {
        if (((1 << obj.layer) & layers) != 0)
        {
            return true;
        }

        return false;
    }
     private void EnemyDie(object sender, System.EventArgs e)
    {
        _healthSystem.OnHealthStateMin -= EnemyDie;
        Destroy(gameObject);
    }
}
