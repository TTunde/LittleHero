using UnityEngine;

public class swamp : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = GetComponent<PlayerMovement>();
        if (collision.GetComponent<PlayerMovement>() != null)
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.drag = 200;
        }
    }
}
