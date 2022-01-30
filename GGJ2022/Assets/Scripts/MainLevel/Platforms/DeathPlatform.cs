using UnityEngine;

namespace MainLevel.Platforms
{
    public class DeathPlatform : Platform
    {
        private void Start()
        {
            type = Type.Death;
        }
    }
}
