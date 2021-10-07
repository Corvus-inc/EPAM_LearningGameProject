﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private Transform targetForLook;
    [SerializeField] private HealthSystem _healthSystem;
    [SerializeField] private LayerMask _layerEnemy;

    [Range(0, 1000)]
    [SerializeField]
    private int HealthPlayer;
    [Range(1, 20)]
    [SerializeField]
    private float mooveSpeed = 5f;
    [Range(1, 5)]
    [SerializeField]
    private float boostSpeedRate;

    private void Start()
    {
        _healthSystem = new HealthSystem(HealthPlayer);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
            _healthSystem.Damage(20);
        if (Input.GetKeyDown(KeyCode.Space)) _healthSystem.Heal(20);

        CharacterMove();
        LookAtTargetforPlayer(targetForLook);
    }

    private void OnCollisionEnter(Collision collision)
    {
        bool check = CheckLayerMask(collision.gameObject, _layerEnemy);
        if (check)
        {
            _healthSystem.Damage(20);
        }
    }

    private void CharacterMove()
    {
        float currentSpeed = mooveSpeed;

        var boostAxis = Input.GetAxis("BoostSpeed");
        var verticalAxis = Input.GetAxis("Vertical");
        var horiontalAxis = Input.GetAxis("Horizontal");

        if (boostAxis != 0)
        {
            currentSpeed *= boostSpeedRate * boostAxis;
        }

        if (verticalAxis != 0)
        {
            Vector3 toTranslate = Vector3.forward * verticalAxis * Time.deltaTime * currentSpeed;
            transform.Translate(toTranslate, Space.World);
        }

        if (horiontalAxis != 0)
        {
            Vector3 toTranslate = Vector3.right * horiontalAxis * Time.deltaTime * currentSpeed;
            transform.Translate(toTranslate, Space.World);
        }
    }

    private void LookAtTargetforPlayer(Transform targetForLook)
    { 
        Vector3 positionsForLook = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        targetForLook.position = new Vector3(positionsForLook.x, transform.position.y, positionsForLook.y);

        transform.LookAt(targetForLook);       
    }
    
    public HealthSystem GetHealthSystem()
    {
        return _healthSystem;
    }

    private bool CheckLayerMask(GameObject obj, LayerMask layers) //For this method need creating service
    {
        if (((1 << obj.layer) & layers) != 0)
        {
            return true;
        }

        return false;
    }
}