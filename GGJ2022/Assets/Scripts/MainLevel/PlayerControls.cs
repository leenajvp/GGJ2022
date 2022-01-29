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

        [Header("Movement")]
        [SerializeField] private Rigidbody2D rb;
        private bool onMovingPlat, onIcePlat, onHotPlat;

        [Header("Circle Behaviour")]
        [SerializeField] private CircleCollider2D circleCol;
        private Vector2 rollDir;
    
        [Header("Square Behaviour")]
        [SerializeField] private BoxCollider2D squareCol;
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
                Color colour = sr.material.color;
                float fade = colour.a - (meltSpeed * Time.deltaTime);
            
                colour = new Color(colour.r, colour.g, colour.b, fade);
                sr.material.color = colour;
            
                if (colour.a <= 0)
                    RestartLevel();
            }
        
            transform.eulerAngles = new Vector3(0, 0, (rollDir == Vector2.right ? -10 : 10));
            rb.constraints = slide ? RigidbodyConstraints2D.None : RigidbodyConstraints2D.FreezePositionX;
        }

    
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!state) return;
            
            if (other.transform.rotation.z < 0) rollDir = Vector2.right;
            else if (other.transform.rotation.z > 0) rollDir = Vector2.left;
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
                    else onIcePlat = true;
                    break;
                
                case Platform.Type.Hot:
                    onHotPlat = true;
                    break;
                
                case Platform.Type.Moving:
                    MovingPlatform movingPlatform = other.gameObject.GetComponent<MovingPlatform>();
                    if (!state)
                    {
                        transform.SetParent(null);
                        return;
                    }
                    onMovingPlat = true;
                    movingPlatform.CanMove = true;
                    transform.SetParent(movingPlatform.PlayerHolder.transform);
                    break;
                
                case Platform.Type.Spike:
                    RestartLevel();
                    break;
                
                case Platform.Type.Bonus:
                    BonusPlatform bonusPlatform = other.gameObject.GetComponent<BonusPlatform>();
                    bonusPlatform.LoadBonusLevel();
                    break;
                
                case Platform.Type.Win:
                    WinPlatform winPlatform = other.gameObject.GetComponent<WinPlatform>();
                    winPlatform.EndLevel();
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
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
                    break;
                
                case Platform.Type.Hot:
                    onHotPlat = false;
                    break;
                
                case Platform.Type.Moving:
                    MovingPlatform movingPlatform = other.gameObject.GetComponent<MovingPlatform>();
                    movingPlatform.CanMove = false;
                    onMovingPlat = false;
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
