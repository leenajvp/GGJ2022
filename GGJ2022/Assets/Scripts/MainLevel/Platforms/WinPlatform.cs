using UnityEngine;

namespace MainLevel.Platforms
{
    public class WinPlatform : Platform
    {
        [SerializeField] private GameObject winPanel;
        
        private void Start()
        {
            type = Type.Win;
            winPanel.SetActive(false);
        }

        public void EndLevel()
        {
            Time.timeScale = 0;
            int nextLevel = PlayerPrefs.GetInt("CurrentLevel") + 1;
            PlayerPrefs.SetInt("CurrentLevel", nextLevel);
            winPanel.SetActive(true);
        }
    }
}
