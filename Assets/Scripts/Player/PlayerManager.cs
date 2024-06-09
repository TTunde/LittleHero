using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    //el�rhet� legyen m�shonnan:
    public static PlayerManager instance;
    public Transform respawnPoint;
    [SerializeField] private GameObject playerPrefab;
    public GameObject currentPlayer;


    private void Awake()
    {
        instance = this;
        PlayerRespawn();
    }

    public void PlayerRespawn()
    {
        if (currentPlayer == null)
            currentPlayer = Instantiate(playerPrefab, respawnPoint.position, transform.rotation);
    }
}
