using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawningController : MonoBehaviour
{
    [SerializeField] private int enemyCounter;
    [SerializeField] private float coolDown;
    [SerializeField] private GameObject enemy;

    void Start()
    {
        StartCoroutine(DoCheck());
    }

    void CounterEnemyCheck()
    {
        if (enemyCounter > 0) { 
            var inst = Instantiate(enemy, transform.position, Quaternion.identity, transform);
            inst.transform.localPosition = new Vector3(Random.Range(-10, 10), 0f, Random.Range(-10, 10));

            enemyCounter--;
        }
    }

    IEnumerator DoCheck()
    {
        for (; ; )
        {
            CounterEnemyCheck();
            yield return new WaitForSeconds(2f);
        }
    }


}
