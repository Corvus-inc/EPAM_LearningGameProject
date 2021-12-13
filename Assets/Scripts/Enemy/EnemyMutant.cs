using System;
using Sounds;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyMutant : MonoBehaviour, IEnemy
{
    [Range(0,1000)] [SerializeField] private int startHealthEnemy;

    [FormerlySerializedAs("prefabWeapon")] [SerializeField] private GameObject prefabWeaponHandler;
    [SerializeField] private LayerMask layerBullet;
    [SerializeField] private HealthBarEnemy healthBar;
    
    private WeaponSystem _weaponSystem;
    private GameObject _currentWeapon;
    private IHealthSystem _healthSystem;
    private WeaponHolder _enemyWeaponHolder;

    private IHealthBar HealthBarEnemy => healthBar;

    private void Awake()
    {
        var newWeapon = Instantiate(prefabWeaponHandler, transform).GetComponent<WeaponHolder>();
        var go = newWeapon.gameObject;
        _weaponSystem = new WeaponSystem(go, transform, 1000);
        _currentWeapon = go;
        _enemyWeaponHolder = _currentWeapon.GetComponent<WeaponHolder>();
        
        
        _healthSystem = new HealthSystem(startHealthEnemy);
        _healthSystem.OnHealthStateMin += EnemyDie;
        HealthBarEnemy.HealthSystem = _healthSystem;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        bool check = GameUtils.Utils.CheckLayerMask(collision.gameObject, layerBullet);
        
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

    public void Attack(Transform targetPosition)
    {
        _enemyWeaponHolder.AimLookAt(targetPosition.position + Vector3.up*5);
        _enemyWeaponHolder.GunCurrent.UsageWeapon();
    }

    private void EnemyDie(object sender, EventArgs e)
    {
        _healthSystem.OnHealthStateMin -= EnemyDie;
        Destroy(gameObject);    
    }
}
