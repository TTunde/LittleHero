using UnityEngine;

public class Player : MonoBehaviour
{
    Animator myAnimator;
    Rigidbody2D myRigidBody;
    bool isDamaged;
    private void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        myAnimator.SetBool("isDamaged", isDamaged);
        //   isDamaged = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (FindObjectOfType<TrapController>() != null)
        {
            isDamaged = true;
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, 2);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (FindObjectOfType<TrapController>() != null)
            isDamaged = false;
    }

}
