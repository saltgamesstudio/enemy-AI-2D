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

        private static readonly int Combo1 = Animator.StringToHash("Attack1");
        private static readonly int Combo2 = Animator.StringToHash("Attack2");
        private static readonly int Combo3 = Animator.StringToHash("Attack3");
        
        private bool _isAttack;
        private bool _isFlipX;

        private int[] _comboAttack = new int[3];
        private int _attackCount;

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

            if (Input.GetMouseButtonDown(0) && !_isAttack)
            {
                _attackCount++;
                _attackCount = _attackCount > 3 ? _attackCount = 1 : _attackCount++;

                switch (_attackCount)
                {
                    case 1:
                        StartCoroutine(Attack1());
                        break;
                    case 2:
                        StartCoroutine(Attack2());
                        break;
                    case 3:
                        StartCoroutine(Attack3());
                        break;
                    default:
                        Debug.Log("Attack Count Out Of Range() ");
                        break;
                }

                Debug.Log("Attack Count : " +_attackCount);
            }
        }

        private void FixedUpdate()
        {
            if (!_isAttack)
            {
                rigidbody2d.MovePosition(rigidbody2d.position + _movement * (moveSpeed * Time.fixedDeltaTime));
            }
        }

        private IEnumerator Attack1()
        {
            _isAttack = true;
            
            animator.SetBool(Combo1, _isAttack);
            
            Debug.Log("Attack 1");

            yield return new WaitForSeconds(0.2f);
            
            _isAttack = false;
            
            animator.SetBool(Combo1, _isAttack);
        }
        
        private IEnumerator Attack2()
        {
            _isAttack = true;
            
            animator.SetBool(Combo2, _isAttack);
            
            Debug.Log("Attack 2");

            yield return new WaitForSeconds(0.2f);
            
            _isAttack = false;
            
            animator.SetBool(Combo2, _isAttack);
        }
        
        private IEnumerator Attack3()
        {
            _isAttack = true;
            
            animator.SetBool(Combo3, _isAttack);
            
            Debug.Log("Attack 3");

            yield return new WaitForSeconds(0.2f);
            
            _isAttack = false;
            
            animator.SetBool(Combo3, _isAttack);
        }
    }
}
