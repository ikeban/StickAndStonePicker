using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //[SerializeField]
    private float moveSpeed = 14000.0f;
    private Rigidbody playerRb;

    private Vector3 forward, right;

    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler( new Vector3(0, 90, 0) ) * forward;
        playerRb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        if (Input.anyKey)
        {
            Move();
        }
    }

    void Move()
    {
        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");
        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        transform.forward = heading;
        playerRb.AddForce(heading * moveSpeed);
        //transform.position += upMovement;
        //transform.position += rightMovement;

    }
}
