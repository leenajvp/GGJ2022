using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

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

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}

