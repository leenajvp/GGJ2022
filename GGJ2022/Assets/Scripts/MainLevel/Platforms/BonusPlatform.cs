using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainLevel.Platforms
{
    public class BonusPlatform : Platform
    {
        private void Start()
        {
            type = Type.Bonus;
        }

        public void LoadBonusLevel()
        {
            PlayerPrefs.SetInt("BonusLevel", 1);
            SceneManager.LoadScene("Bonus");
        }
    }
}
