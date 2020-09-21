using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    private Vector3 offset = new Vector3(-20.5f, 12.5f, -10.4f);
    private Vector3 rotation = new Vector3(30, 45, 0);

    public GameObject player;

    void Start()
    {
        
    }


    void LateUpdate()
    {
        FollowPlayerObject();
    }

    void FollowPlayerObject()
    {
        transform.position = player.transform.position + offset;
        transform.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
    }
}
