using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerControls : MonoBehaviour
{
    [Header("Duality")]
    private bool state;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite circleSprite, squareSprite;

    private void Start()
    {
        ChangeState(false);

        if (!spriteRenderer) spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ChangeState();
        
        switch (state)
        {
            // Circle
            case false:
                break;
            
            // Square
            case true:
                break;
        }
    }

    private void ChangeState()
    {
        state = !state;
        spriteRenderer.sprite = state ? squareSprite : circleSprite;
    }

    private void ChangeState(bool newState)
    {
        state = newState;
        spriteRenderer.sprite = state ? squareSprite : circleSprite;
    }
}
