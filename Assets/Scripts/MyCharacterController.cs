using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCharacterController : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRigidbody;

    [Range(1, 10)]
     public int mooveSpeed;

    private Vector3 moveTo;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.W))
        {
            playerRigidbody.velocity = Vector3.forward * mooveSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerRigidbody.velocity = -Vector3.right * mooveSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            playerRigidbody.velocity = -Vector3.forward * mooveSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerRigidbody.velocity = Vector3.right * mooveSpeed;
        }
    }
}
