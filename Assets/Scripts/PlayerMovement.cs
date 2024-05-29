using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    Animator myAnimator;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float jumpForce = 2f;
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

    [Header("Wall check and slide")]
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
        }
        else if (isGroundDetected)
        {
            Jump();
        }
        else if (canDoubleJump)
        {
            canDoubleJump = false;
            Jump();
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
        isWallDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDirection, wallCheckDistance, whatIsGround);

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
    }
}

