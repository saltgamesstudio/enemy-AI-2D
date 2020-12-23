using UnityEngine;

namespace Character.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public PlayerAttack playerAttack;
        
        public float moveSpeed = 5f;

        public Rigidbody2D rigidbody2d;
        public Animator animator;

        private Vector2 movement;
        
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        private static readonly int Speed = Animator.StringToHash("Speed");
        
        private bool isFlipX;

        public void Movement()
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            
            animator.SetFloat(Horizontal, movement.x);
            animator.SetFloat(Vertical, movement.y);
            animator.SetFloat(Speed, movement.sqrMagnitude);

            if (movement.x < 0) gameObject.GetComponent<SpriteRenderer>().flipX = true;
            if (movement.x > 0) gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        public void DoMovement()
        {
            if (!playerAttack.isAttack)
            {
                rigidbody2d.MovePosition(rigidbody2d.position + movement * (moveSpeed * Time.fixedDeltaTime));
            }
        }
    }
}
