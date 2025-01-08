
using Unity.Cinemachine;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] AudioSource boulderSmash;
    [SerializeField] float shakeModifier = 10f;
    CinemachineImpulseSource cinemachineImpulseSource;
    [SerializeField] float collisionCooldown = 1f;
    float collisionTimer = 0f;
    private void Update() {
        collisionTimer+= Time.deltaTime;
    }
    private void Awake() {
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }
    private void OnCollisionEnter(Collision other) {
        if(collisionTimer<collisionCooldown) return;
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        float shakeIntensity = (1f/distance) * shakeModifier;
        shakeIntensity = Mathf.Min(shakeIntensity, 1f);
        cinemachineImpulseSource.GenerateImpulse(shakeIntensity);
        boulderSmash.Play();
        collisionTimer = 0f;
    }
}
