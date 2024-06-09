using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator myAnimator;
    protected Rigidbody2D myRigidbody;

    [SerializeField] protected int facingDirection = 1;

    [Header("Collision detect info")]
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected float wallCheckDistance;

    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected Transform wallCheck;

    protected bool isWallDetected;
    protected bool isGroundDetected;

    [Header("Anims")]
    protected bool isMoving;

    protected virtual void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }
    public void Damage()
    {
        myAnimator.SetTrigger("gotHit");
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMovement player = collision.collider.GetComponent<PlayerMovement>();
        if (player != null)
        {

            if (player.transform.position.x > transform.position.x)
                player.Knockback(1);
            else if (player.transform.position.x < transform.position.x)
                player.Knockback(-1);
        }
    }

    protected virtual void Flip()
    {
        facingDirection = facingDirection * -1;
        transform.Rotate(0, 180, 0);
    }

    protected virtual void CollisionChecks()
    {
        isGroundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        isWallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, wallCheckDistance, whatIsGround);
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance * facingDirection, wallCheck.position.y));
    }
    protected virtual void AnimationController()
    {
        myAnimator.SetBool("isMoving", isMoving);
    }
}
