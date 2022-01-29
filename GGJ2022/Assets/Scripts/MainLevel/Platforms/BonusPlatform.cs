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
            SceneManager.LoadScene("Bonus");
        }
    }
}
