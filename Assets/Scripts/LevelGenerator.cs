using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class LevelGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] CameraController cameraController;
    [SerializeField] GameObject[] chunkPrefabs;
    [SerializeField] GameObject checkpointChunkPrefab;
    [SerializeField] Transform chunkParent;
    [SerializeField] ScoreManager scoreManager;
    [Header("LevelSettings")]
    [SerializeField] int startingChunksAmount=20;
    [SerializeField] int checkpointChunkInterval=10;
    [Tooltip("This value should be the same as chunk prefab length")]
    [SerializeField] float chunkLength = 10f;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float minSpeed = 8f;
    [SerializeField] float maxSpeed = 20f;
    [SerializeField] float minGravity = -5.81f;
    [SerializeField] float maxGravity = -19.81f;
    List<GameObject> chunks = new List<GameObject>();
    int chunkSpawned = 0;
    private void Start() 
    {
        SpawnChunks();
    }
    private void Update()
    {
        MoveChunks();
    }

    public void AdjustMoveSpeed(float speed)
    {
        float newMoveSpeed = moveSpeed + speed;
        newMoveSpeed = Mathf.Clamp(newMoveSpeed, minSpeed, maxSpeed);
        if(moveSpeed != newMoveSpeed)
        {
            moveSpeed=newMoveSpeed;
            float newGravityZ = Physics.gravity.z - speed;
            newGravityZ = Mathf.Clamp(newGravityZ, minGravity, maxGravity);
            Physics.gravity = new Vector3(Physics.gravity.x,Physics.gravity.y , newGravityZ);
            cameraController.ChangeCameraFOV(speed);
        }
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
        GameObject chunkToSpawn = ChooseChunkToSpawn();
        GameObject newChunkGO = Instantiate(chunkToSpawn, zAxis, Quaternion.identity, chunkParent);
        chunks.Add(newChunkGO);
        Chunk newChunk = newChunkGO.GetComponent<Chunk>();
        newChunk.Init(this, scoreManager);
        chunkSpawned++;
    }

    private GameObject ChooseChunkToSpawn()
    {
        GameObject chunkToSpawn;
        if (chunkSpawned % checkpointChunkInterval == 0 && chunkSpawned != 0)
        {
            chunkToSpawn = checkpointChunkPrefab;
        }
        else
        {
            chunkToSpawn = chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];
        }

        return chunkToSpawn;
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
