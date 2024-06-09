using UnityEngine;

public class EnemyMushroom : Enemy
{
    [SerializeField] float idleTime = 2;
    [SerializeField] float idleCounter;

    [SerializeField] private float speed;
    protected override void Start()
    {
        base.Start();
        facingDirection = -1;
    }

    private void Update()
    {
        if (idleCounter <= 0)
        {
            myRigidbody.velocity = new Vector2(speed * facingDirection, myRigidbody.velocity.y);
        }
        else
        {
            myRigidbody.velocity = Vector2.zero;
        }

        idleCounter -= Time.deltaTime;


        if (myRigidbody.velocity.x != 0)
            isMoving = true;
        else
            isMoving = false;

        AnimationController();
        CollisionChecks();

        if (isWallDetected || !isGroundDetected)
        {
            idleCounter = idleTime;
            Flip();
        }
    }

    //private float RandomNumber()
    //{
    //    float num = Random.Range(1, 6);
    //    return num;
    //}
}
