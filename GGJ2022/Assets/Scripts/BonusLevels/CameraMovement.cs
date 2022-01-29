using UnityEngine;

namespace BonusLevel
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private bool oneWay;

        private void Update()
        {
            if (oneWay)
            {
                if (player.position.y > transform.position.y)
                {
                    transform.position = new Vector3(transform.position.x, player.position.y, transform.position.z);
                }
            }

            else 
            {
                transform.position = new Vector3(transform.position.x, player.position.y, transform.position.z);
            }
        }
    }
}
