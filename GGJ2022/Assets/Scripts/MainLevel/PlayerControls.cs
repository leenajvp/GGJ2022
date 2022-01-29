using System;
using MainLevel.Platforms;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainLevel
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D), typeof(BoxCollider2D))]
    public class PlayerControls : MonoBehaviour
    {
        [SerializeField] private Transform start;
        
        [Header("Duality")]
        [SerializeField] private SpriteRenderer sr;
        [SerializeField] private Sprite circleSprite, squareSprite;
        private bool state;

        public bool State => state;

        [Header("Movement")]
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private LayerMask groundLayer;
        private bool onMovingPlat, onIcePlat, onHotPlat;
        public bool grounded;

        [Header("Circle Behaviour")]
        [SerializeField] private CircleCollider2D circleCol;
        [SerializeField] private Sprite coldCircle;
        private Vector2 rollDir = Vector2.right;
    
        [Header("Square Behaviour")]
        [SerializeField] private BoxCollider2D squareCol;
        [SerializeField] private Sprite hotSquare;
        [SerializeField] private bool slide;
        [SerializeField] private float iceSlideSpeed;
        [SerializeField] private float meltSpeed;
    

        private void Start()
        {
            if (!sr) sr = GetComponent<SpriteRenderer>();
            if (!rb) rb = GetComponent<Rigidbody2D>();
            if (!circleCol) circleCol = GetComponent<CircleCollider2D>();
            if (!squareCol) squareCol = GetComponent<BoxCollider2D>();

            if (start) transform.position = start.position;
            ChangeState(false);
            gameObject.SetActive(true);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                ChangeState();
        }

        private void FixedUpdate()
        {
            Debug.DrawRay(transform.position, Vector3.down, Color.green);
            
            switch (state)
            {
                case false:
                    grounded = Physics2D.Raycast(transform.position, Vector2.down, circleCol.bounds.extents.y + 0.1f, groundLayer);
                    CircleBehaviour();
                    break;
            
                case true:
                    grounded = Physics2D.Raycast(transform.position, Vector2.down, squareCol.bounds.extents.y + 0.1f, groundLayer);
                    SquareBehaviour();
                    break;
            }
        }

        private void RestartLevel()
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
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

            if (onHotPlat)
            {
                var material = sr.material;
                Color colour = material.color;
                float fade = colour.a - (meltSpeed * Time.deltaTime);
            
                colour = new Color(colour.r, colour.g, colour.b, fade);
                material.color = colour;
            
                if (colour.a <= 0)
                    RestartLevel();
            }
        
            transform.eulerAngles = new Vector3(0, 0, (rollDir == Vector2.right ? -10 : 10));
            if (grounded)
                rb.constraints = slide ? RigidbodyConstraints2D.None : RigidbodyConstraints2D.FreezePositionX;
            else if (!grounded)
                rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }

    
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.rotation.z < 0) rollDir = Vector2.right;
            else if (other.transform.rotation.z > 0) rollDir = Vector2.left;
            
            var platform = other.gameObject.GetComponent<Platform>();
            if (!platform) return;

            switch (platform.ThisType)
            {
                case Platform.Type.Ice:
                    onIcePlat = true;
                    sr.sprite = !state ? coldCircle : squareSprite;
                    break;
                
                case Platform.Type.Hot:
                    onHotPlat = true;
                    sr.sprite = !state ? circleSprite : hotSquare;
                    break;
                
                case Platform.Type.Moving:
                    onMovingPlat = true;
                    break;
                
                case Platform.Type.Spike:
                    RestartLevel();
                    break;
                
                case Platform.Type.Bonus:
                    other.gameObject.GetComponent<BonusPlatform>().LoadBonusLevel();
                    break;
                
                case Platform.Type.Win:
                    other.gameObject.GetComponent<WinPlatform>().EndLevel();
                    break;
                
                default:
                    break;
            }
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            var platform = other.gameObject.GetComponent<Platform>();
            if (!platform) return;

            switch (platform.ThisType)
            {
                case Platform.Type.Ice:
                    IcePlatform icePlatform = other.gameObject.GetComponent<IcePlatform>();
                    if (!state) icePlatform.Melt();
                    break;
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            var platform = other.gameObject.GetComponent<Platform>();
            if (!platform) return;

            switch (platform.ThisType)
            {
                case Platform.Type.Ice:
                    onIcePlat = false;
                    sr.sprite = !state ? circleSprite : squareSprite;
                    break;
                
                case Platform.Type.Hot:
                    onHotPlat = false;
                    sr.sprite = !state ? circleSprite : squareSprite;
                    break;
                
                case Platform.Type.Moving:
                    onMovingPlat = false;
                    break;
                
                default:
                    break;
            }
        }
    }
}
