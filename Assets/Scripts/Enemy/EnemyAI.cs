using System;
using System.Collections;
using UnityEngine;
using Pathfinding;

namespace Enemy
{
    public class EnemyAI : MonoBehaviour
    {
        public Transform target;
        public Transform enemyGfx;

        public Animator animator;
        
        [HideInInspector] public bool isAttack;
        
        public float speed = 200f;
        public float nextWaypointDistance = 3f;

        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Speed = Animator.StringToHash("Speed");
        
        private static readonly int Combo1 = Animator.StringToHash("Attack1");
        private static readonly int Combo2 = Animator.StringToHash("Attack2");
        private static readonly int Combo3 = Animator.StringToHash("Attack3");
        
        private Path _path;
        private Seeker _seeker;
        private Rigidbody2D _rigidbody;
        
        private int _currentWayPoint;
        private int _attackCount;
        
        private float _timer;

        private bool _cooldown;

        // Start is called before the first frame update
        void Start()
        {
            _seeker = GetComponent<Seeker>();
            _rigidbody = GetComponent<Rigidbody2D>();

            InvokeRepeating(nameof(UpdatePath), 0, 0.5f);
        }

        void Update()
        {
            if (_cooldown)
            {
                if (_timer < 1)
                {
                    _timer += Time.deltaTime;
                }
                else
                {
                    _timer = 0;
                    _cooldown = false;
                }
            }
        }

        void FixedUpdate()
        {
            if (_path == null) return;

            if (_currentWayPoint >= _path.vectorPath.Count)
            {
                Debug.Log("Must Stop!");
                
                animator.SetFloat(Speed, 0);

                Attack();
                
                return;
            }
            
            Movement();
        }

        private void UpdatePath()
        {
            if (_seeker.IsDone()) _seeker.StartPath(_rigidbody.position, target.position, OnPathComplete);
        }

        private void Movement()
        {
            Vector2 direction = ((Vector2) _path.vectorPath[_currentWayPoint] - _rigidbody.position).normalized;

            Vector2 force = direction * (speed * Time.deltaTime);

            _rigidbody.AddForce(force);
                
            float distance = Vector2.Distance(_rigidbody.position, _path.vectorPath[_currentWayPoint]);

            if (distance < nextWaypointDistance) _currentWayPoint++;

            animator.SetFloat(Speed, 1);
            
            if (_rigidbody.velocity.x >= 0.01f)
            {
                enemyGfx.localScale = new Vector3(-1, 1, 1);
            }
            else if (_rigidbody.velocity.x <= -0.01f)
            {
                enemyGfx.localScale = new Vector3(1, 1, 1);
            }
            
            animator.SetFloat(Horizontal, enemyGfx.localScale.x);
        }

        private void Attack()
        {
            if (!_cooldown)
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
                
                _cooldown = true;
            }
        }
        
        private void OnPathComplete(Path path)
        {
            if (!path.error)
            {
                _path = path;
                
                _currentWayPoint = 0;
            }
        }

        private IEnumerator Attack1()
        {
            isAttack = true;
            
            animator.SetBool(Combo1, isAttack);
            
            Debug.Log("Attack 1");

            yield return new WaitForSeconds(0.2f);
            
            isAttack = false;
            
            animator.SetBool(Combo1, isAttack);
        }
        
        private IEnumerator Attack2()
        {
            isAttack = true;
            
            animator.SetBool(Combo2, isAttack);
            
            Debug.Log("Attack 2");

            yield return new WaitForSeconds(0.2f);
            
            isAttack = false;
            
            animator.SetBool(Combo2, isAttack);
        }
        
        private IEnumerator Attack3()
        {
            isAttack = true;
            
            animator.SetBool(Combo3, isAttack);
            
            Debug.Log("Attack 3");

            yield return new WaitForSeconds(0.2f);
            
            isAttack = false;
            
            animator.SetBool(Combo3, isAttack);
        }
    }
}
