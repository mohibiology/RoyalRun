using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] Transform chunkParent;
    [SerializeField] int startingChunksAmount=10;
    [SerializeField] float chunkLength = 10f;
    private void Start() 
    {
        float spawnPosition=transform.position.z;
        for (int i = 0; i < startingChunksAmount; i++)
        {
            Vector3 zAxis = new Vector3(transform.position.x,transform.position.y,spawnPosition);
            Instantiate(chunkPrefab,zAxis,Quaternion.identity,chunkParent);
            spawnPosition+=chunkLength;
        }

    }
}
