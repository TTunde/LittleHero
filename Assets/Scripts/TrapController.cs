using UnityEngine;


public class TrapController : MonoBehaviour
{
    [Header("Audio info")]
    [SerializeField] public AudioClip audioSFX;
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() != null)
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();
            if (player.transform.position.x > transform.position.x)
                player.Knockback(1);
            else if (player.transform.position.x < transform.position.x)
                player.Knockback(-1);
            else
                player.Knockback(0);
        }
    }
}
