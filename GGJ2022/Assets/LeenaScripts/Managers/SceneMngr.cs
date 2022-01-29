using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMngr : MonoBehaviour
{
    public void NewGame()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt("CurrentLevel", 1);
        SceneManager.LoadScene(1);
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        SceneManager.LoadScene(currentLevel);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame() => Application.Quit();
        
}

