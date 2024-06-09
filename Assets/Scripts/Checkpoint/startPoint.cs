using UnityEngine;

public class startPoint : MonoBehaviour
{

    private void Awake()
    {
        PlayerManager.instance.respawnPoint = transform;
        PlayerManager.instance.PlayerRespawn();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() != null)
        {
            if (collision.transform.position.x > transform.position.x)
                GetComponent<Animator>().SetTrigger("touch");
        }

    }
}
