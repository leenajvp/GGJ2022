using System;
using UnityEngine;

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
    [SerializeField] private CircleCollider2D circleCol;
    [SerializeField] private BoxCollider2D boxCol;
    [SerializeField] private float currentSpeed, circleSpeed, squareSpeed;
    [SerializeField] private float acceleration;

    private void Start()
    {
        if (!sr) sr = GetComponent<SpriteRenderer>();
        if (!rb) rb = GetComponent<Rigidbody2D>();
        if (!circleCol) circleCol = GetComponent<CircleCollider2D>();
        if (!boxCol) boxCol = GetComponent<BoxCollider2D>();
        
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
            // Circle
            case false:
                CircleBehaviour();
                break;
            
            // Square
            case true:
                SquareBehaviour();
                break;
        }
    }

    private void ChangeState()
    {
        state = !state;

        if (!state)
        {
            sr.sprite = circleSprite;
            circleCol.enabled = true;
            boxCol.enabled = false;
            rb.simulated = true;
        }
        else
        {
            sr.sprite = squareSprite;
            boxCol.enabled = true;
            circleCol.enabled = false;
        }
    }

    private void ChangeState(bool newState)
    {
        state = newState;

        if (!state)
        {
            sr.sprite = circleSprite;
            circleCol.enabled = true;
            boxCol.enabled = false;
            rb.simulated = true;
        }
        else
        {
            sr.sprite = squareSprite;
            boxCol.enabled = true;
            circleCol.enabled = false;
        }
    }

    private void CircleBehaviour()
    {
        if (currentSpeed <= circleSpeed)
            currentSpeed += acceleration * Time.deltaTime;
        
        rb.velocity = new Vector2(0, -currentSpeed);
    }

    private void SquareBehaviour()
    {
        if (currentSpeed >= squareSpeed)
            currentSpeed -= acceleration * Time.deltaTime;
        
        // Completely stop square when slowed down
        if (currentSpeed <= squareSpeed)
            rb.simulated = false;
        
        rb.velocity = new Vector2(0, -currentSpeed);
    }
}
