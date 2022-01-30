using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainLevel
{
    public class LevelManager : MonoBehaviour
    {
        public void RestartLevel()
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

        public void ReturnToMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void LoadNextLevel()
        {
            int nextLevel = PlayerPrefs.GetInt("CurrentLevel") + 1;
            PlayerPrefs.SetInt("CurrentLevel", nextLevel);
            SceneManager.LoadScene(nextLevel);
        }
    }
}
