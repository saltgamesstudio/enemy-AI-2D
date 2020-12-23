using System;

namespace Character.Player
{
    public class Player : Character
    {
        private PlayerMovement playerMovement;
        private PlayerAttack playerAttack;

        void Start()
        {
            playerMovement = gameObject.GetComponent<PlayerMovement>();
            playerAttack = gameObject.GetComponent<PlayerAttack>();
        }
        
        void Update()
        {
            Movement();
            Attack();
        }

        void FixedUpdate()
        {
            playerMovement.DoMovement();
        }

        public override void Movement()
        {
            playerMovement.Movement();
        }

        public override void Attack()
        {
            playerAttack.Attack();
        }
    }
}
