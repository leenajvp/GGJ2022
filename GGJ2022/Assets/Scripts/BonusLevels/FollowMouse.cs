using UnityEngine;

namespace BonusLevel
{
    [RequireComponent(typeof(SwapCharacter))]
    public class FollowMouse : MonoBehaviour
    {
        [Header("Text to Display Before Start")]
        [SerializeField] private GameObject introText;
        [Header("Player's Mouse Follow Speed")]
        [SerializeField] private float speed;
        [Header("Check if Level Completed")]
        public bool levelCompleted;

        private bool started;
        private Vector3 mousePos;
        private SwapCharacter checkDisabled;

        void Start()
        {
            introText.SetActive(true);
            started = false;
            checkDisabled = GetComponent<SwapCharacter>();
            levelCompleted = false;
        }

        void Update()
        {
            if (Input.anyKey)
            {
                started = true;
                introText.SetActive(false);
            }


            if (!checkDisabled.disabled && !levelCompleted)
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
