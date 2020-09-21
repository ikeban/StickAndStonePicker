using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollectableUpDownAnim : MonoBehaviour
{
    private float animationSpeed = 1.0f;
    private float animationRange = 0.2f;
    private float animationDirection = 1f;

    private float cumulativeYPosition = 0f;


    private float baseYPosition = 0f;

    // Start is called before the first frame update
    void Start()
    {
        baseYPosition = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeAnimationDirectionIfNeeded();

        float distanceProgressWithTime = Time.deltaTime * animationSpeed;
        cumulativeYPosition += distanceProgressWithTime * animationDirection;

        float newYPosition = baseYPosition + cumulativeYPosition;
        transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
    }

    void ChangeAnimationDirectionIfNeeded()
    {
        if (cumulativeYPosition > animationRange)
        {
            animationDirection = -1f;
        }

        if (cumulativeYPosition < -animationRange) {
            animationDirection = 1f;
        }
    }
}
