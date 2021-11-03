using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int _enemyCounter;
    [SerializeField] private float _coolDown;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Transform healthBarHolder;

    void Start()
    {
        StartCoroutine(DoCheck());
    }

    void CounterEnemyCheck()
    {
        if (_enemyCounter > 0)
        {
            Enemy instEmemy = Instantiate(_enemy, transform.position, Quaternion.identity, transform);
            instEmemy.healthBarHolder = healthBarHolder;
            instEmemy.transform.localPosition = new Vector3(Random.Range(-10, 10), _enemy.transform.position.y, Random.Range(-10, 10));
            _enemyCounter--;
        }
    }

    IEnumerator DoCheck()
    {
        for (; ; )
        {
            CounterEnemyCheck();
            yield return new WaitForSeconds(_coolDown);
        }
    }
}
