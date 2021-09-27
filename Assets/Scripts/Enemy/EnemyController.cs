using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;

    private SphereCollider enemyTrigger;
    private Transform target;

    void Awake()
    {
        enemyTrigger = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null) transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (CheckLayerMask(other.gameObject, layerMask))
        {
            target = other.transform;
            //enemyTrigger.enabled = false;
            enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        target = null;
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
