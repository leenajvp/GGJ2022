using UnityEngine;

namespace BonusLevel
{
    [RequireComponent(typeof(SwapCharacter))]
    public class FollowMouse : MonoBehaviour
    {
        [SerializeField] private GameObject introText;
        [SerializeField] private float speed;
        private bool started;
        private Vector3 mousePos;
        private SwapCharacter checkDisabled;

        void Start()
        {
            introText.SetActive(true);
            started = false;
            checkDisabled = GetComponent<SwapCharacter>();
        }

        void Update()
        {
            if (Input.anyKey)
            {
                started = true;
                introText.SetActive(false);
            }
                

            if (!checkDisabled.disabled)
                FollowMouseZ();
        }

        private void FollowMouseZ()
        {
            if (started)
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = transform.position.z;
                mousePos.y = transform.position.y;
                transform.position = Vector3.MoveTowards(transform.position, mousePos, speed * Time.deltaTime);
            }
        }
    }
}
