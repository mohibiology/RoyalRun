using UnityEngine;

public class Apple : Pickup
{
    [SerializeField] float increaseMoveSpeed = 2f;
    LevelGenerator  levelGenerator;

    private void Start() 
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
    }
    protected override void OnPickup()
    {
        levelGenerator.AdjustMoveSpeed(increaseMoveSpeed);
    }
}
