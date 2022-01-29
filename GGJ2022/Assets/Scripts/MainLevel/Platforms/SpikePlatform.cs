using System;
using UnityEngine;

namespace MainLevel.Platforms
{
    public class SpikePlatform : Platform
    {
        private void Start()
        {
            type = Type.Spike;
        }
    }
}
