using Cinemachine;
using UnityEngine;


public class CameraManager : MonoBehaviour
{
    [SerializeField] GameObject myCamera;


    private void Start()
    {
        myCamera.GetComponent<CinemachineVirtualCamera>().Follow = GameObject.Find("PlayerMovement").transform;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() != null)
        {
            myCamera.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() != null)
        {
            myCamera.SetActive(false);
        }
    }
}
