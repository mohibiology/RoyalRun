using UnityEngine;

public class Pickup : MonoBehaviour
{
    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log(other.gameObject.tag);
        }
    }
}
