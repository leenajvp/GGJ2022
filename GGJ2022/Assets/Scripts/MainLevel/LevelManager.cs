using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainLevel
{
    public class LevelManager : MonoBehaviour
    {
        public void RestartLevel()
        {
            PlayerPrefs.SetInt("BonusLevel", 0);
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

            if (nextLevel == SceneManager.sceneCountInBuildSettings - 1)
            {
                nextLevel = 1;
                PlayerPrefs.SetInt("CurrentLevel", nextLevel);
                SceneManager.LoadScene(0);
            }
            else
            {
                PlayerPrefs.SetInt("CurrentLevel", nextLevel);
                SceneManager.LoadScene(nextLevel);
            }
        }
    }
}
