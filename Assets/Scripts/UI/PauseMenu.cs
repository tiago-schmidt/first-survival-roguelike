using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject _pauseMenu;
    private ScoreManager _scoreManager;

    void Start()
    {
        _scoreManager = GameObject.FindGameObjectWithTag(Tags.SCORE_MANAGER).GetComponent<ScoreManager>();
        _pauseMenu.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
        {
            SwitchPauseMenu();
        }
    }

    public void SwitchPauseMenu()
    {
        _pauseMenu.SetActive(!_pauseMenu.activeSelf);
        Time.timeScale = _pauseMenu.activeSelf ? 0 : 1;
    }

    public void QuitGame()
    {
        _scoreManager.AddGoldToPlayerFromScore();
        DataPersistenceManager.Instance.SaveGame();
        SceneManager.LoadScene(Scenes.MAIN_MENU);
    }
}
