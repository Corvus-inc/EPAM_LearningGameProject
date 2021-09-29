using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private HealthBar _healsBarEnemy;
    float size = 1;

    private SphereCollider _enemyTrigger;
    private Transform _target;

    void Awake()
    {
        _enemyTrigger = GetComponent<SphereCollider>();
    }

    void Update()
    {
        _healsBarEnemy.SetColour(Color.red);
        _healsBarEnemy.SetSize(size);
        size -= 0.1f * Time.deltaTime;

        if (_target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, Time.deltaTime);
            transform.LookAt(_target);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (CheckLayerMask(other.gameObject, _layerMask))
        {
            _target = other.transform;
            //enemyTrigger.enabled = false;
            enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        _target = null;
    }

    private bool CheckLayerMask(GameObject obj, LayerMask layers)
    {
        if (((1 << obj.layer) & layers) != 0)
        {
            return true;
        }

        return false;
    }
}
