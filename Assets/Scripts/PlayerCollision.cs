using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float collisionCooldown = 1f;
    [SerializeField] float decreaseMoveSpeed = -2f;
    const string hitString = "Hit";
    float collisionCooldownTime = 0f;
    LevelGenerator levelGenerator;
    void Start() 
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
    }
    void Update() 
    {
        collisionCooldownTime += Time.deltaTime;
    }
    void OnCollisionEnter(Collision other) 
    {
        if(collisionCooldownTime < collisionCooldown)
            return;
        animator.SetTrigger(hitString);
        levelGenerator.AdjustMoveSpeed(decreaseMoveSpeed);
        collisionCooldownTime = 0f;
    }
}
