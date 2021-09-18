using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCharacterController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    [SerializeField] private Transform targetForLook;

    [Range(1, 20)]
    public float mooveSpeed = 5f;
    [Range(1, 5)]
    public float boostSpeedRate;

    void Update()
    {
        Movement();
        LookAtTargetforPlayer(targetForLook);
    }

    private void Movement()
    {
        float currentSpeed = mooveSpeed;

        if (Input.GetAxis("BoostSpeed") != 0)
        {
            currentSpeed *= boostSpeedRate * Input.GetAxis("BoostSpeed");
        }

        if (Input.GetAxis("Vertical") != 0)
        {
            Vector3 vec = Vector3.forward * Input.GetAxis("Vertical") * Time.deltaTime * currentSpeed;
            playerTransform.position += vec;
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            Vector3 vec = Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * currentSpeed;
            playerTransform.position += vec;
        }
    }

    private void LookAtTargetforPlayer(Transform targetForLook)
    { 
        Vector3 positionsForLook = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        targetForLook.position = new Vector3(positionsForLook.x, transform.position.y, positionsForLook.y);
        transform.LookAt(targetForLook);       
    }
}
