using UnityEngine;

namespace MainLevel.Platforms
{
    public class Platform : MonoBehaviour
    {
        public enum Type {Platform, Ice, Hot, Moving, Spike, Bonus, Win, Death}
        protected Type type = Type.Platform;

        public Type ThisType => type;
    }
}
