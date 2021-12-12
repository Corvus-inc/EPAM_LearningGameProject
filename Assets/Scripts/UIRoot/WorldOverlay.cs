using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldOverlay : MonoBehaviour
{
    public  List<ISpawningWithHealth> ZombieSpawnings { private get; set; }
    public Dictionary<HealthBarEnemy, Transform> ListBars { private get; set; }
    
    [SerializeField] private HealthBarEnemy zombieHealthBar;
    [SerializeField] private Vector3 offsetZombieBar;
    [SerializeField] private Transform zombieBars;
    
    private void Update()
    {
        foreach (var bar in ListBars)
        {
            bar.Key.FollowTo(ListBars[bar.Key], offsetZombieBar);
        }
    }

    public void CreateZombieHealthBar(IHaveHealth health)
    {
        var bar = Instantiate(zombieHealthBar, zombieBars);
        bar.HealthSystem = health.MyHealthSystem;
        bar.SetColour(Color.red);
        bar.HealthSystem.OnHealthStateMin += (sender, args) => 
        { 
            Destroy(bar.gameObject);
            ListBars.Remove(bar);
        };
        ListBars.Add(bar, health.GetHealthOwner().transform);
    }
}
