using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //[SerializeField]
    private float moveSpeed = 14000.0f;
    private Rigidbody playerRb;
    [SerializeField] private ParticleSystem dustParticle = null;
    private ParticleSystem.EmissionModule dustEmissionModule;
    private PlayerStatsTracker playerStatsTrackerScript;

    private Vector3 forward, right;

    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler( new Vector3(0, 90, 0) ) * forward;
        playerRb = GetComponent<Rigidbody>();
        dustEmissionModule = dustParticle.emission;
        dustEmissionModule.enabled = false;
        playerStatsTrackerScript = GetComponent<PlayerStatsTracker>();
    }


    void Update()
    {
        if (playerStatsTrackerScript.gameIsActive)
        {
            Move();
            
        }
        else
        {
            ShowDustIfPlayerMoves(Vector3.zero, Vector3.zero);
        }
    }

    void Move()
    {
        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");
        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        if (heading != Vector3.zero)
        {
            transform.forward = heading; 
        }
        playerRb.AddForce(heading * moveSpeed);

        ShowDustIfPlayerMoves(rightMovement, upMovement);
    }

    void ShowDustIfPlayerMoves(Vector3 rightMovement, Vector3 upMovement)
    {
        if (rightMovement != Vector3.zero || upMovement != Vector3.zero)
        {
            dustEmissionModule.enabled = true;

        }
        else
        {
            dustEmissionModule.enabled = false;
        }
    }
}
