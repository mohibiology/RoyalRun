using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    void OnTriggerEnter(Collider other) 
    {
        Destroy(other.gameObject);
    }
}
