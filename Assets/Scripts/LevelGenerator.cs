using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] Transform chunkParent;
    [SerializeField] int startingChunksAmount=10;
    [SerializeField] float chunkLength = 10f;
    List<GameObject> chunks = new List<GameObject>();
    [SerializeField] float moveSpeed = 10f;
    private void Start() 
    {
        SpawnChunks();
    }
    private void Update()
    {
        MoveChunks();
    }
    void SpawnChunks()
    {
        for (int i = 0; i < startingChunksAmount; i++)
        {
            SpawnChunk();
        }
    }

    private void SpawnChunk()
    {
        float spawnPositionZ = CalculateSpawnPositionZ();
        Vector3 zAxis = new Vector3(transform.position.x, transform.position.y, spawnPositionZ);
        GameObject newChunk = Instantiate(chunkPrefab, zAxis, Quaternion.identity, chunkParent);
        chunks.Add(newChunk);
    }

    float CalculateSpawnPositionZ()
    {
        float spawnPositionZ;
        if(chunks.Count==0)
            spawnPositionZ=transform.position.z;
        else
            spawnPositionZ=chunks[chunks.Count-1].transform.position.z + chunkLength;
        return spawnPositionZ;
    }
    void MoveChunks()
    {
        for(int i=0;i<chunks.Count;i++)
        {
            GameObject chunk = chunks[i];
            chunks[i].transform.Translate(-transform.forward * (moveSpeed * Time.deltaTime));
            if(chunk.transform.position.z <= Camera.main.transform.position.z - chunkLength)
            {
                chunks.Remove(chunk);
                Destroy(chunk);
                SpawnChunk();
            }
        }
    }
}
