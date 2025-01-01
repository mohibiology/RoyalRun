using UnityEngine;

public class Apple : Pickup
{
    [SerializeField] float increaseMoveSpeed = 2f;
    LevelGenerator levelGenerator;

    public void Init(LevelGenerator levelGenerator) 
    {
        this.levelGenerator = levelGenerator;
    }
    protected override void OnPickup()
    {
        levelGenerator.AdjustMoveSpeed(increaseMoveSpeed);
    }
}
