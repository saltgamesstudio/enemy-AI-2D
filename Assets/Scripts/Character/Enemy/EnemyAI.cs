using Pathfinding;
using UnityEngine;

namespace Character.Enemy
{
    public class EnemyAI : Character
    {
        public Transform target;
        public Transform enemyGfx;

        public Animator animator;
        
        [HideInInspector] public bool isAttack;
        
        public float speed = 200f;
        public float nextWaypointDistance = 3f;

        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Speed = Animator.StringToHash("Speed");

        private const int DoMovement = 1;
        private const int DontMovement = 0;

        private EnemyAttack enemyAttack;
        
        private Path path;
        private Seeker seeker;
        private Rigidbody2D rb;
        
        private int currentWayPoint;

        // Start is called before the first frame update
        void Start()
        {
            seeker = GetComponent<Seeker>();
            rb = GetComponent<Rigidbody2D>();

            enemyAttack = gameObject.GetComponent<EnemyAttack>();

            InvokeRepeating(nameof(UpdatePath), 0, 0.5f);
        }

        void FixedUpdate()
        {
            if (path == null) return;

            if (currentWayPoint >= path.vectorPath.Count)
            {
                animator.SetFloat(Speed, DontMovement);

                Attack();
                
                return;
            }
            
            Movement();
        }

        private void UpdatePath()
        {
            if (seeker.IsDone()) seeker.StartPath(rb.position, target.position, OnPathComplete);
        }

        private void OnPathComplete(Path paths)
        {
            if (!paths.error)
            {
                path = paths;
                
                currentWayPoint = 0;
            }
        }

        public override void Movement()
        {
            Vector2 direction = ((Vector2) path.vectorPath[currentWayPoint] - rb.position).normalized;

            Vector2 force = direction * (speed * Time.deltaTime);

            rb.AddForce(force);
                
            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

            if (distance < nextWaypointDistance) currentWayPoint++;

            animator.SetFloat(Speed, DoMovement);
            
            if (rb.velocity.x >= 0.01f)
            {
                enemyGfx.localScale = new Vector3(-1, 1, 1);
            }
            else if (rb.velocity.x <= -0.01f)
            {
                enemyGfx.localScale = new Vector3(1, 1, 1);
            }
            
            animator.SetFloat(Horizontal, enemyGfx.localScale.x);
        }

        public override void Attack()
        {
            enemyAttack.Attack();
        }
    }
}
