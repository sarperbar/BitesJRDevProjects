using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] objectPrefabs;
    private GameManager_4 gameManager;

    // spawn range
    private float spawnRangeX =15;
    private float spawnRangZ = 15;
    private float spawnRangey = 1;
    
    private float lastAcceleration;
    private int stepCount;
    private float lastStepTime;
    int time=0;
    float previousVelocityZ;
    
    
    void Start()
    {
        gameManager=GameObject.Find("GameManager_4").GetComponent<GameManager_4>();
    
    }
    void Update()
    {
        
        float velocityZDifference = Mathf.Abs(gameManager.velocityZ - previousVelocityZ);

        if (gameManager.velocityZ < 0.6f && gameManager.velocityZ > 0.1f && velocityZDifference < 0.05f)
        {
            if (Time.timeSinceLevelLoad - time > Random.Range(5, 15))
            {
                RandomPrefab();
                time = (int)Time.timeSinceLevelLoad;
            }
        }


        previousVelocityZ = gameManager.velocityZ;
    }
    
    void RandomPrefab()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX),Random.Range(-spawnRangey, spawnRangey), Random.Range(-spawnRangZ, spawnRangZ));
        int prefabIndex = Random.Range(0, objectPrefabs.Length);
        Instantiate(objectPrefabs[prefabIndex], spawnPos, objectPrefabs[prefabIndex].transform.rotation);
    }
}