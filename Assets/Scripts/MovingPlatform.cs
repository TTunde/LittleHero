using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    Animator myAnimator;

    [SerializeField] Transform[] movePoint;
    [SerializeField] float speed;
    [SerializeField] float cooldown;

    float cooldownTimer;
    int movePointIndex;

    bool isWorking;

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
        transform.position = movePoint[0].position;
    }

    private void Update()
    {
        cooldownTimer -= Time.deltaTime;
        isWorking = cooldownTimer < 0;
        myAnimator.SetBool("isWorking", isWorking);

        if (isWorking)
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoint[movePointIndex].position, speed * Time.deltaTime);
        }

        if (Vector2.Distance(transform.position, movePoint[movePointIndex].position) < 0.15f)
        {
            cooldownTimer = cooldown;
            movePointIndex++;

            if (movePointIndex >= movePoint.Length)
                movePointIndex = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() != null)
            collision.transform.SetParent(transform);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() != null)
            collision.transform.SetParent(null);
    }

}
