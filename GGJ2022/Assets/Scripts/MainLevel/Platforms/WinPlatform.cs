using System;
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
            winPanel.SetActive(true);
        }
    }
}
