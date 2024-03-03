using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeCycleManager : MonoBehaviour
{
    [SerializeField] private GameObject _timer;
    [SerializeField] private GameObject _gameOverScreen;
    private ScoreManager _scoreManager;

    private void Start()
    {
        _scoreManager = GameObject.FindGameObjectWithTag(Tags.SCORE_MANAGER).GetComponent<ScoreManager>();
        _timer.SetActive(true);
        Time.timeScale = 1;
    }

    public void ShowWinningScreen()
    {
        _scoreManager.AddGoldToPlayerFromScore();
        Time.timeScale = 0;

        // TODO - alterar para winningScreen quando criar
        _gameOverScreen.SetActive(true);
    }

    public void ShowGameOverScreen()
    {
        _scoreManager.AddGoldToPlayerFromScore();
        Time.timeScale = 0;
        _gameOverScreen.SetActive(true);
    }

    public void Reload()
    {
        SceneManager.LoadScene(Scenes.MAIN_SCENE);
    }

    public void Quit()
    {
        DataPersistenceManager.Instance.SaveGame();
        SceneManager.LoadScene(Scenes.MAIN_MENU);
    }
}
