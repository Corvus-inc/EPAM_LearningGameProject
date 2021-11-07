using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform moveTargetPosition;

    private NavMeshAgent _navMeshAgent;
    private Vector3 _startingPosition;
    private Vector3 _roamPosition;
    private float _nextShootTime;
    private State _state;

    private enum State
    {
        Roaming,
        ChaseTarget,
        GoingBackToStart
    }

    private void Awake()
    {
        moveTargetPosition = GameObject.Find("Player")?.transform;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _state = State.Roaming;
    }

    private void Start()
    {
        _startingPosition = transform.position;
        _roamPosition = GetRoamingPosition();
    }

    private void Update()
    {
        switch (_state)
        {
            default:
            case State.Roaming:
                MoveTo(_roamPosition);
                var reachedPositionDistance = 1f;
                if (Vector3.Distance(transform.position, _roamPosition) < reachedPositionDistance)
                {
                    _roamPosition = GetRoamingPosition();
                }

                FindTarget();
                break;
            case State.ChaseTarget:
                MoveTo(moveTargetPosition.position);
                float attackRange = 3f;
                if (Vector3.Distance(transform.position, moveTargetPosition.position) < attackRange)
                {
                    if (Time.time > _nextShootTime)
                    {
                        MoveTo(transform.position);
                        Debug.Log("Attack");
                        float fireRate = .1f;
                        _nextShootTime = Time.time + fireRate;
                    }
                }

                float stopChaseDistance = 8f;
                if (Vector3.Distance(transform.position, moveTargetPosition.position) > stopChaseDistance)
                {
                    _state = State.GoingBackToStart;
                }
                break;
            case State.GoingBackToStart:
                MoveTo(_startingPosition);
                reachedPositionDistance = 1f;
                if (Vector3.Distance(transform.position, _startingPosition) < reachedPositionDistance)
                {
                    _state = State.Roaming;
                }
                break;
        }
    }

    private Vector3 GetRoamingPosition()
    {
        return _startingPosition + GameUtils.Utils.GetRandomDir() * Random.Range(5f, 10f);
    }

    private void FindTarget()
    {
        float targetRange = 5f;
        if (Vector3.Distance(transform.position, moveTargetPosition.position) < targetRange)
        {
            _state = State.ChaseTarget;
        }
    }
    //Create method with timer
    private void MoveTo(Vector3 position)
    {
        _navMeshAgent.destination = position;
    }
}