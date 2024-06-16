using UnityEngine;

public class MainMenuBackground : MonoBehaviour
{
    [SerializeField] MeshRenderer mesh;
    [SerializeField] Vector2 backgroundSpeed;
    private void Update()
    {
        mesh.material.mainTextureOffset += backgroundSpeed * Time.deltaTime;
    }
}
