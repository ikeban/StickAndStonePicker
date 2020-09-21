using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableAnim : MonoBehaviour
{
    private float rotationSpeed = 80.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
