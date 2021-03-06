using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
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
