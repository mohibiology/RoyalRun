using UnityEngine;
using System.Collections.Generic;

public class Chunk : MonoBehaviour
{
    [SerializeField] GameObject fencePrefab;
    [SerializeField] GameObject applePrefab;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] float[] lanes = {-2.5f, 0f, 2.5f};
    List<int> availableLanes = new List<int> {0, 1, 2};
    [SerializeField] float appleSpawnChance = 0.2f;
    [SerializeField] float coinSpawnChance = 0.5f;
    float coinSeparationLength = 2f;
    bool isAppleSpawned = false;
    LevelGenerator levelGenerator;
    ScoreManager scoreManager;

    public void Init(LevelGenerator levelGenerator, ScoreManager scoreManager ) 
    {
        this.levelGenerator = levelGenerator;
        this.scoreManager = scoreManager;
    }

    void Start()
    {
        SpawnFence();
        SpawnApple();
        SpawnCoins();
    }
    void SpawnFence()
    {
        int fenceToSpawn = Random.Range(0, lanes.Length);

        for (int i = 0; i < fenceToSpawn; i++)
        {
            int selectedLane = SelecteLane();
            Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);
            Instantiate(fencePrefab, spawnPosition, Quaternion.identity, this.transform);
        }

    }
    void SpawnApple()
    {
        if(availableLanes.Count<=0 || Random.value > appleSpawnChance)
            return;
        int selectedLane = SelecteLane();
        isAppleSpawned = true;
        Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);
        Apple newApple = Instantiate(applePrefab, spawnPosition, Quaternion.identity, this.transform).GetComponent<Apple>();
        newApple.Init(levelGenerator);
    }
    void SpawnCoins()
    {
        if(availableLanes.Count<=0 || Random.value > coinSpawnChance || isAppleSpawned)
            return;
        int selectedLane = SelecteLane();
        int coinsToSpawn = Random.Range(1, 6);
        float topOfChunkZPos = transform.position.z + (coinSeparationLength*2f);
        for (int i = 0; i < coinsToSpawn; i++)
        {
            float spawnPositionZ = topOfChunkZPos - (i * coinSeparationLength);
            Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, spawnPositionZ);
            Coin newCoin =Instantiate(coinPrefab, spawnPosition, Quaternion.identity, this.transform).GetComponent<Coin>();   
            newCoin.Init(scoreManager); 
        }
    }

    int SelecteLane()
    {
        int randomIndex = Random.Range(0, availableLanes.Count);
        int selectedLane = availableLanes[randomIndex];
        availableLanes.RemoveAt(randomIndex);
        return selectedLane;
    }
}
