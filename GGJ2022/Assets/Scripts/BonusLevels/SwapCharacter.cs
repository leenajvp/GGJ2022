using UnityEngine;

namespace BonusLevel
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SwapCharacter : MonoBehaviour
    {
        [Header("Default Character Sprites")]
        [SerializeField] private Sprite ball;
        [SerializeField] private Sprite cube;

        [Header("Game Over Character Sprites")]
        [SerializeField] private Sprite ballFall;
        [SerializeField] private Sprite cubeMelt;
        private SpriteRenderer currentSprite;

        [Header("Checking Current State")]
        public bool isBall;
        public bool disabled;

        private void Start()
        {
            currentSprite = GetComponent<SpriteRenderer>();
            isBall = true;
            disabled = false;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!isBall)
                {
                    isBall = true;
                }

                else
                {
                    isBall = false;
                }
            }

            if (disabled)
                PlayerDeadSprites();

            else
                UpdateSprite();
        }

        private void UpdateSprite()
        {
            if (isBall)
                currentSprite.sprite = ball;

            else
                currentSprite.sprite = cube;
        }

        private void PlayerDeadSprites()
        {
            if (isBall)
                currentSprite.sprite = ballFall;

            else
                currentSprite.sprite = cubeMelt;
        }

    }
}

