using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMutant : MonoBehaviour
{
    [Range(0,1000)] [SerializeField] private int startHealthEnemy;
    
    [SerializeField] private LayerMask layerBullet;
    [SerializeField] private HealthBarEnemy healthBarEnemy;
    
    private HealthSystem _healthSystem;

    private void Awake()
    {
        _healthSystem = new HealthSystem(startHealthEnemy);
        _healthSystem.OnHealthStateMin += EnemyDie;
        healthBarEnemy.HealthSystem = _healthSystem;
        // healthBarEnemy.SetColour(Color.green);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        bool check = GameUtils.Utils.CheckLayerMask(collision.gameObject, layerBullet);
        
        if (check)
        {
            IBullet bullet = collision.gameObject.GetComponent<IBullet>();
            int damage = bullet.GetBulletDamage();
            _healthSystem.Damage(damage);
            
            StopCoroutine(bullet.DeactivatingBullet(0));
            bullet.DeactivatingBullet();
        }    
    }

    private void EnemyDie(object sender, EventArgs e)
    {
        _healthSystem.OnHealthStateMin -= EnemyDie;
        Destroy(gameObject);    }
}
