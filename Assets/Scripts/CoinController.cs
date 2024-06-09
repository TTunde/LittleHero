using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
        Destroy(gameObject);
    }
}
