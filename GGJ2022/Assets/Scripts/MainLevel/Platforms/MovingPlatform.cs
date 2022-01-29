using System;
using UnityEngine;

namespace MainLevel.Platforms
{
    public class MovingPlatform : Platform
    {
        [SerializeField] private float speed;
        [SerializeField] private Vector2 pointA, pointB;
        [SerializeField] private bool pingPong;
        [SerializeField] private bool canMove;
        [SerializeField] private GameObject playerHolder;

        public bool CanMove
        {
            get => canMove;
            set => canMove = value;
        }

        public GameObject PlayerHolder
        {
            get => playerHolder;
            set => playerHolder = value;
        }

        private void Start()
        {
            type = Type.Moving;
            transform.position = pointA;
        }

        private void FixedUpdate()
        {
            if (!canMove) return;

            if (pingPong)
            {
                float time = Mathf.PingPong(Time.time * speed, 1);
                transform.position = Vector2.Lerp(pointA, pointB, time);
            }
            else
                transform.position = Vector2.Lerp(transform.position, pointB, Time.deltaTime * speed);

        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            PlayerControls player = other.gameObject.GetComponent<PlayerControls>();
            
            if (player)
            {
                if (player.State)
                {
                    canMove = true;
                    other.collider.transform.SetParent(transform, true);
                }
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            PlayerControls player = other.gameObject.GetComponent<PlayerControls>();
            
            if (player)
            {
                canMove = false;
                other.collider.transform.SetParent(null, true);
            }
        }
    }
}
