using System;
using System.Collections;
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
        private static readonly int Melee = Animator.StringToHash("Attack_Melee");

        private bool _isAttackMelee;
        
        // Update is called once per frame
        void Update()
        {        
            _movement.x = Input.GetAxisRaw("Horizontal");
            _movement.y = Input.GetAxisRaw("Vertical");
            
            animator.SetFloat(Horizontal, _movement.x);
            animator.SetFloat(Vertical, _movement.y);
            animator.SetFloat(Speed, _movement.sqrMagnitude);

            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(AttackMelee());
            }
        }

        private void FixedUpdate()
        {
            if (!_isAttackMelee)
            {
                rigidbody2d.MovePosition(rigidbody2d.position + _movement * (moveSpeed * Time.fixedDeltaTime));
            }
        }

        private IEnumerator AttackMelee()
        {
            animator.SetBool(Melee, true);
            _isAttackMelee = true;
            Debug.Log("Attack!!");

            yield return new WaitForSeconds(0.3f);
            
            animator.SetBool(Melee, false);
            _isAttackMelee = false;
            Debug.Log("Stop Attack!!");
        }
    }
}
