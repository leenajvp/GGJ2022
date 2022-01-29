using System;
using UnityEngine;

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
            // Load bonus level
        }
    }
}
