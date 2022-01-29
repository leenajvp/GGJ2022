using UnityEngine;
using UnityEngine.UI;

namespace MainLevel
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D), typeof(BoxCollider2D))]
    public class PlayerControls : MonoBehaviour
    {
        [Header("Duality")]
        private bool state;
        [SerializeField] private SpriteRenderer sr;
        [SerializeField] private Sprite circleSprite, squareSprite;

        [Header("Movement")]
        [SerializeField] private Rigidbody2D rb;
        private bool onMovingPlat, onIcePlat;

        [Header("Circle Behaviour")]
        [SerializeField] private CircleCollider2D circleCol;
        private Vector2 rollDir;
    
        [Header("Square Behaviour")]
        [SerializeField] private BoxCollider2D squareCol;
        [SerializeField] private bool slide;
        [SerializeField] private float iceSlideSpeed;
    

        private void Start()
        {
            if (!sr) sr = GetComponent<SpriteRenderer>();
            if (!rb) rb = GetComponent<Rigidbody2D>();
            if (!circleCol) circleCol = GetComponent<CircleCollider2D>();
            if (!squareCol) squareCol = GetComponent<BoxCollider2D>();
        
            ChangeState(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                ChangeState();
        }

        private void FixedUpdate()
        {
            switch (state)
            {
                case false:
                    CircleBehaviour();
                    break;
            
                case true:
                    SquareBehaviour();
                    break;
            }
        }

        // Toggle state
        private void ChangeState()
        {
            state = !state;
            sr.sprite = state ? squareSprite : circleSprite;

            // Circle
            if (!state)
            {
                rb.constraints = RigidbodyConstraints2D.None;
                circleCol.enabled = true;
                squareCol.enabled = false;
                rb.velocity = rollDir;
            }
            // Square
            else
            {
                squareCol.enabled = true;
                circleCol.enabled = false;
            }
        }

        // Set specific state
        private void ChangeState(bool newState)
        {
            state = newState;
            sr.sprite = state ? squareSprite : circleSprite;

            // Circle
            if (!state)
            {
                rb.constraints = RigidbodyConstraints2D.None;
                circleCol.enabled = true;
                squareCol.enabled = false;
                rb.velocity = rollDir;
            }
            // Square
            else
            {
                squareCol.enabled = true;
                circleCol.enabled = false;
            }
        }

        private void CircleBehaviour()
        {
            rollDir = rb.velocity.x > 0 ? Vector2.right : Vector2.left ;
        }

        private void SquareBehaviour()
        {
            if (onMovingPlat) return;

            if (onIcePlat)
            {
                rb.velocity = rollDir * iceSlideSpeed;
                return;
            }
        
            transform.eulerAngles = new Vector3(0, 0, (rollDir == Vector2.right ? -10 : 10));
            rb.constraints = slide ? RigidbodyConstraints2D.None : RigidbodyConstraints2D.FreezePositionX;
        }

    
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.rotation.z < 0) rollDir = Vector2.right;
            else if (other.transform.rotation.z > 0) rollDir = Vector2.left;
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            MovingPlatform movingPlatform = other.gameObject.GetComponent<MovingPlatform>();
            IcePlatform icePlatform = other.gameObject.GetComponent<IcePlatform>();

            if (movingPlatform)
            {
                if (!state)
                {
                    transform.SetParent(null);
                    return;
                }
        
                onMovingPlat = true;
                movingPlatform.CanMove = true;
                transform.SetParent(movingPlatform.PlayerHolder.transform);
            }
            else if (icePlatform)
            {
                if (!state) icePlatform.Melt();
                else onIcePlat = true;
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            MovingPlatform movingPlatform = other.gameObject.GetComponent<MovingPlatform>();
            IcePlatform icePlatform = other.gameObject.GetComponent<IcePlatform>();

            if (movingPlatform)
            {
                movingPlatform.CanMove = false;
                onMovingPlat = false;
            }
            else if (icePlatform)
                onIcePlat = false;
        }
    }
}
