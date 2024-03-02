using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string mainScene = "Main Scene";

    public void NewGame()
    {
        SceneManager.LoadScene(mainScene);
    }
}
