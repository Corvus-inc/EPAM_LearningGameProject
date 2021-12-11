using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldOverlay : MonoBehaviour
{
    [SerializeField] private HealthBarEnemy zombieHealthBar;
    [SerializeField] private Vector3 offsetZombieBar;
    [SerializeField] private SpawnerCircle spawner;
    [SerializeField] private Transform zombieBars;
    
    private ISpawningWithHealth _onceSpawningWithHealth;
    private Dictionary<HealthBarEnemy, Transform> _listBars;
    private void Awake()
    {
        _onceSpawningWithHealth = Instantiate(spawner);
        _listBars = new Dictionary<HealthBarEnemy, Transform>();

        _onceSpawningWithHealth.spawnedObject += CreateZombieHealthBar;
    }

    private void Update()
    {
        foreach (var bar in _listBars)
        {
            bar.Key.FollowTo(_listBars[bar.Key], offsetZombieBar);
        }
    }

    private void CreateZombieHealthBar(IHaveHealth health)
    {
        var bar = Instantiate(zombieHealthBar, zombieBars);
        bar.HealthSystem = health.MyHealthSystem;
        _listBars.Add(bar, health.GetHealthOwner().transform);
    }
}
