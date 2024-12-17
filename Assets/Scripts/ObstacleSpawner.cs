using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] float ObstacleSpawnTime=1f;
    int obstacleSpawn = 0;
    private void Start() 
    {
        StartCoroutine(SpawnObstacleRoutine());
    }
    IEnumerator SpawnObstacleRoutine()
    {
        while(obstacleSpawn<5)
        {
            yield return new WaitForSeconds(ObstacleSpawnTime);
            Instantiate(obstaclePrefab,transform.position, Quaternion.identity);
            obstacleSpawn++;
        }
    }
}
