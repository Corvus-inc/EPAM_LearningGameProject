using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour, ISpawningWithHealth
{
    public event Action<IHaveHealth> spawnedObject;
    
    [SerializeField] private int enemyCounter;
    [SerializeField] private float coolDown;
    [SerializeField] private Enemy enemy;

    void Start()
    {
        StartCoroutine(DoCheck());
    }

    void CounterEnemyCheck()
    {
        if (enemyCounter > 0)
        {
            Enemy instEmemy = Instantiate(enemy, transform.position, Quaternion.identity, transform);
            spawnedObject?.Invoke(instEmemy);
            instEmemy.transform.localPosition = new Vector3(Random.Range(-10, 10), enemy.transform.position.y, Random.Range(-10, 10));
            enemyCounter--;
        }
    }

    IEnumerator DoCheck()
    {
        for (; ; )
        {
            CounterEnemyCheck();
            yield return new WaitForSeconds(coolDown);
        }
    }
}
