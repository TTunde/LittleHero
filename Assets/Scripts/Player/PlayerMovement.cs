using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    Animator myAnimator;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float jumpForce = 6.5f;
    [SerializeField] float doubleJumpForce = 2f;
    float defaultJumpForce;
    bool isMoving;
    float xInput;
    bool isFacingRight = true;
    bool canDoubleJump = true;
    bool canMove;

    [Header("Ground check")]
    [SerializeField] float groundCheckRadius;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] Transform groundCheck;
    bool isGroundDetected;
    [SerializeField] Transform enemyCheck;
    [SerializeField] float enemyCheckRadius;
    [SerializeField] Vector3 enemyCheckPosition;

    [Header("Wall check and slide")]
    [SerializeField] LayerMask whatIsWall;
    [SerializeField] float wallCheckDistance;
    bool isWallDetected;
    bool canWallSlide;
    bool isWallSliding;
    [SerializeField] Vector2 wallJumpDirection;

    int facingDirection = 1;
    float yInput;

    [Header("Knocked")]
    [SerializeField] Vector2 knockbackDirection;
    [SerializeField] float knockbackTime;
    [SerializeField] float knockbackProtectionTime;
    bool isKnocked;

    [Header("Audio info")]
    [SerializeField] AudioClip jumpSFX;


    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        defaultJumpForce = jumpForce;
    }
    void Update()
    {
        AnimationController();
        if (isKnocked) { return; }
        FlipController();
        CollisionCheck();
        InputChecks();

        if (isGroundDetected)
        {
            canDoubleJump = true;
            canMove = true;
        }

        if (canWallSlide)
        {
            isWallSliding = true;
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, myRigidBody.velocity.y * 0.1f);
        }
        Move();
        EnemyCheck();
    }

    private void EnemyCheck()
    {
        Collider2D[] hitedColliders = Physics2D.OverlapCircleAll(enemyCheck.position, enemyCheckRadius);
        //Collider2D[] hitedColliders = Physics2D.OverlapBoxAll(enemyCheck.position, enemyCheckPosition, 2);
        foreach (var enemyCollider in hitedColliders)
        {
            if (enemyCollider.GetComponent<Enemy>() != null)
            {
                if (myRigidBody.velocity.y < 0)
                {
                    enemyCollider.GetComponent<Enemy>().Damage();
                    Jump();
                }
            }
        }
    }

    void WallJump()
    {
        canMove = false;
        myRigidBody.velocity = new Vector2(4 * -facingDirection, jumpForce);
        Flip();
    }
    void Flip()
    {
        facingDirection = facingDirection * -1;
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);
    }
    private void FlipController()
    {
        if (xInput < 0 && isFacingRight)
            Flip();
        else if (xInput > 0 && !isFacingRight)
            Flip();
    }

    void InputChecks()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        if (yInput < 0)
        {
            canWallSlide = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpActions();
        }
    }
    public void Knockback(int direction)
    {
        isKnocked = true;
        myRigidBody.velocity = new Vector2(knockbackDirection.x * direction, knockbackDirection.y);

        Invoke("CancelKnockback", knockbackTime);
    }
    public void CancelKnockback()
    {
        isKnocked = false;
    }

    void Jump()
    {
        myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
        GetComponent<AudioSource>().PlayOneShot(jumpSFX);
    }

    void JumpActions()
    {
        if (isWallSliding && !isGroundDetected)
        {
            WallJump();
            canDoubleJump = true;
        }
        else if (isGroundDetected)
        {
            Jump();
        }
        else if (canDoubleJump)
        {
            canMove = true;
            jumpForce = doubleJumpForce;
            canDoubleJump = false;
            Jump();
            jumpForce = defaultJumpForce;
        }
        canWallSlide = false;
    }
    void Move()
    {
        if (canMove)
        {
            myRigidBody.velocity = new Vector2(xInput * moveSpeed, myRigidBody.velocity.y);
            if (myRigidBody.velocity.x != 0)
                isMoving = true;
            else
                isMoving = false;
        }
    }

    private void AnimationController()
    {
        myAnimator.SetBool("isKnocked", isKnocked);
        myAnimator.SetBool("isGrounded", isGroundDetected);
        myAnimator.SetFloat("yVelocity", myRigidBody.velocity.y);
        myAnimator.SetBool("isMoving", isMoving);
        myAnimator.SetBool("isWallSliding", isWallSliding);
    }

    private void CollisionCheck()
    {
        isGroundDetected = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        isWallDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDirection, wallCheckDistance, whatIsWall);

        if (isWallDetected && myRigidBody.velocity.y < 0)
            canWallSlide = true;

        if (!isWallDetected)
        {
            isWallSliding = false;
            canWallSlide = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.transform.position, groundCheckRadius);
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + wallCheckDistance * facingDirection, transform.position.y));
        Gizmos.DrawWireSphere(enemyCheck.position, enemyCheckRadius);
        //Gizmos.DrawCube(enemyCheck.position, enemyCheckPosition);
    }
}

