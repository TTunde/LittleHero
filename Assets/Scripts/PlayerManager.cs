using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private GameObject playerPrefab;
    public GameObject currentPlayer;

    private void Awake()
    {
        PlayerRespawn();
    }

    private void PlayerRespawn()
    {
        if (currentPlayer == null)
            currentPlayer = Instantiate(playerPrefab, respawnPoint.position, transform.rotation);
    }
}
