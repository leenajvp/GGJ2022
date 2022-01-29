using UnityEngine;

namespace MainLevel.Platforms
{
    public class MovingPlatform : Platform
    {
        [SerializeField] private float speed;
        [SerializeField] private Vector2 pointA, pointB;
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
        
            float time = Mathf.PingPong(Time.time * speed, 1);
            transform.position = Vector2.Lerp(pointA, pointB, time);
        }
    }
}
