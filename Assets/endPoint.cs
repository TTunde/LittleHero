using UnityEngine;

public class endPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() != null)
        {
            GetComponent<Animator>().SetTrigger("reached");
        }
    }
}
