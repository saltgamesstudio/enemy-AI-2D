using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {

        public float moveSpeed = 5f;

        public Rigidbody2D rigidbody2d;
        public Animator animator;

        private Vector2 _movement;
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        private static readonly int Speed = Animator.StringToHash("Speed");

        // Update is called once per frame
        void Update()
        {        
            _movement.x = Input.GetAxisRaw("Horizontal");
            _movement.y = Input.GetAxisRaw("Vertical");
            
            animator.SetFloat(Horizontal, _movement.x);
            animator.SetFloat(Vertical, _movement.y);
            animator.SetFloat(Speed, _movement.sqrMagnitude);
        }

        private void FixedUpdate()
        {
            rigidbody2d.MovePosition(rigidbody2d.position + _movement * (moveSpeed * Time.fixedDeltaTime));
        }
    }
}
