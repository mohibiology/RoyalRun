using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] obstaclePrefabs;
    [SerializeField] float ObstacleSpawnTime=1f;
    [SerializeField] Transform obstacleParent;
    [SerializeField] float spawnWidth;

    private void Start() 
    {
        StartCoroutine(SpawnObstacleRoutine());
    }
    IEnumerator SpawnObstacleRoutine()
    {
        while(true)
        {
            GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0,obstaclePrefabs.Length)];
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnWidth,spawnWidth),transform.position.y,transform.position.z);
            yield return new WaitForSeconds(ObstacleSpawnTime);
            Instantiate(obstaclePrefab,spawnPosition,Random.rotation,obstacleParent);
        }
    }
}
