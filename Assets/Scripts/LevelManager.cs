using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject levelButton;
    [SerializeField] private Transform levelButtonParent;

    private void Start()
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            GameObject newButton = Instantiate(levelButton, levelButtonParent);
        }
    }
    public void LoadLevel(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}


