using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Range(0,1000)] [SerializeField] private int _startHealthEnemy;

    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private HealthBar _healthBarEnemy;

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
        _healthBarEnemy.Setup(_healthSystem);
        _healthBarEnemy.SetColour(Color.red);//why error in Awake, but working?
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace)) _healthSystem.Damage(20);
        if (Input.GetKeyDown(KeyCode.Space)) _healthSystem.Heal(20);
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
