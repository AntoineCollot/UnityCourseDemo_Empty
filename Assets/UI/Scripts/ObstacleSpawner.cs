using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {

    public GameObject obstaclePrefab;
    public int maxObstacles;

    public float minSpawnDist = 3;
    public float maxSpawnDist = 10;
    float nextSpawnPosition;

	// Use this for initialization
	void Start () {
        nextSpawnPosition += transform.position.z;
    }
	
	// Update is called once per frame
	void Update () {
		if(transform.position.z>=nextSpawnPosition)
        {
            nextSpawnPosition+=Random.Range(minSpawnDist,maxSpawnDist);
            SpawnObstacle(Random.Range(1,maxObstacles));
        }
	}

    void SpawnObstacle(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(obstaclePrefab, transform.position, Random.rotation, null);
        }
    }
}
