using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] collectableItems;
    public GameObject[] powerUps;
    public GameObject nail;

    private float xSpawnRange = 20.0f;
    private float zSpawnRange = 18.0f;

    private float spawnDelay = 1.0f;
    private float powerUpSpawnTime = 8f;
    private float nailSpawnTime = 5f;
    private float collectableSpawnTime = 3f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnNail", spawnDelay, nailSpawnTime);
        InvokeRepeating("SpawnPowerUp", spawnDelay, powerUpSpawnTime);
        InvokeRepeating("SpawnCollectable", spawnDelay, collectableSpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3 getRandomSpawnPosition(float yPosition)
    {
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);
        float randomZ = Random.Range(-zSpawnRange, zSpawnRange);
        return new Vector3(randomX, yPosition, randomZ);
    }

    void SpawnNail()
    {
        float nailYPosition = 0.25f;
        Vector3 nailSpawnPosition = getRandomSpawnPosition(nailYPosition);
        Instantiate(nail, nailSpawnPosition, nail.transform.rotation);
    }

    void SpawnPowerUp()
    {
        float powerUpYPosition = 0.67f;
        Vector3 powerUpSpawnPosition = getRandomSpawnPosition(powerUpYPosition);
        int randomIndex = Random.Range(0, powerUps.Length);
        Instantiate(powerUps[randomIndex], powerUpSpawnPosition, powerUps[randomIndex].transform.rotation);
    }

    void SpawnCollectable()
    {
        float collectableYPosition = 0.5f;
        Vector3 collectableSpawnPosition = getRandomSpawnPosition(collectableYPosition);
        int randomIndex = Random.Range(0, collectableItems.Length);
        Instantiate(collectableItems[randomIndex], collectableSpawnPosition, collectableItems[randomIndex].transform.rotation);
    }


}
