using Unity.VisualScripting;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] float checkpointTimeExtension = 5f;

    GameManager gameManager;

    void Start() 
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }
    void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
            gameManager.IncreaseTime(checkpointTimeExtension);
    }
}
