using UnityEngine;

namespace BonusLevel
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SwapCharacter : MonoBehaviour
    {
        [SerializeField] private Sprite ball;
        [SerializeField] private Sprite cube;
        private SpriteRenderer currentSprite;

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

            UpdateSprite();
        }

        private void UpdateSprite()
        {
            if (isBall)
                currentSprite.sprite = ball;

            else
                currentSprite.sprite = cube;
        }

    }
}

