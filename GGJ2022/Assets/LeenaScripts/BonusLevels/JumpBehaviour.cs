using UnityEngine;

namespace BonusLevel
{
    public class JumpBehaviour : MonoBehaviour
    {
        [SerializeField] private float jumpHeight = 8.0f;
        [SerializeField] private bool grounded;
        private SwapCharacter swapScript;
        private Rigidbody2D rb;

        private void Start()
        {
            rb = transform.parent.GetComponent<Rigidbody2D>();
            swapScript = transform.parent.GetComponent<SwapCharacter>();
        }

        void Update()
        {
            Jump();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            grounded = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            grounded = false;
        }

        private void Jump()
        {
            if (grounded && !swapScript.disabled)
            {
                rb.velocity = Vector2.up * jumpHeight;
            }
        }
    }
}

