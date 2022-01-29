using UnityEngine;

namespace BonusLevel
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Transform player;

        private void Update()
        {
            if (player.position.y > transform.position.y)
            {
                transform.position = new Vector3(transform.position.x, player.position.y, transform.position.z);
            }

        }
    }
}
