using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float collisionCooldown = 1f;
    const string hitString = "Hit";
    float collisionCooldownTime = 0f;

    void Update() 
    {
        collisionCooldownTime += Time.deltaTime;
    }
    void OnCollisionEnter(Collision other) 
    {
        if(collisionCooldownTime < collisionCooldown)
            return;
        animator.SetTrigger(hitString);
        collisionCooldownTime = 0f;
    }
}
