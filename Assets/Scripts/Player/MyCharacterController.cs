using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCharacterController : MonoBehaviour
{
    [SerializeField] private Transform targetForLook;

    [Range(1, 20)]
    [SerializeField]
    private float mooveSpeed = 5f;
    [Range(1, 5)]
    [SerializeField]
    private float boostSpeedRate;

    void Update()
    {
        CharacterMove();
        LookAtTargetforPlayer(targetForLook);
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
}
