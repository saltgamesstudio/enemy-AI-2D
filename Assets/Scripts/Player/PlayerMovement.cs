using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {

        public PlayerAttack playerAttack;
        
        public float moveSpeed = 5f;

        public Rigidbody2D rigidbody2d;
        public Animator animator;

        private Vector2 _movement;
        
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        private static readonly int Speed = Animator.StringToHash("Speed");
        
        private bool _isFlipX;

        // Update is called once per frame
        void Update()
        {        
            _movement.x = Input.GetAxisRaw("Horizontal");
            _movement.y = Input.GetAxisRaw("Vertical");
            
            animator.SetFloat(Horizontal, _movement.x);
            animator.SetFloat(Vertical, _movement.y);
            animator.SetFloat(Speed, _movement.sqrMagnitude);

            if (_movement.x < 0) gameObject.GetComponent<SpriteRenderer>().flipX = true;
            if (_movement.x > 0) gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        private void FixedUpdate()
        {
            if (!playerAttack.isAttack)
            {
                rigidbody2d.MovePosition(rigidbody2d.position + _movement * (moveSpeed * Time.fixedDeltaTime));
            }
        }
    }
}
