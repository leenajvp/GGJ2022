using UnityEngine;

namespace MainLevel.Platforms
{
    public class Platform : MonoBehaviour
    {
        public enum Type {Ice, Hot, Moving, Spike, Bonus, Win}
        protected Type type;

        public Type ThisType => type;
    }
}
