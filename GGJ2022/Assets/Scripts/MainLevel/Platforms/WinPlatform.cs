using System;
using UnityEngine;

namespace MainLevel.Platforms
{
    public class WinPlatform : Platform
    {
        private void Start()
        {
            type = Type.Win;
        }

        public void EndLevel()
        {
            Time.timeScale = 0;
            // End level
        }
    }
}
