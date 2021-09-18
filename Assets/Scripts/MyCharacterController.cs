using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCharacterController : MonoBehaviour
{
    [SerializeField]  private Transform playerTransform;

    [Range(1, 20)]
    public float mooveSpeed = 5f;
    [Range(1, 5)]
    public float boostSpeedRate;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

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
}
