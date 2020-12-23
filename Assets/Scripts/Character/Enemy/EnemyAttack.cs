using System;
using System.Collections;
using UnityEngine;

namespace Character.Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        public Animator animator;
        
        [HideInInspector] public bool isAttack;
        
        private static readonly int Combo1 = Animator.StringToHash("Attack1");
        private static readonly int Combo2 = Animator.StringToHash("Attack2");
        private static readonly int Combo3 = Animator.StringToHash("Attack3");
        
        private const int MAXAttack = 3;
        private const int MINAttack = 1;

        private const float LimitTimer = 1;
        private const float ResetTimer = 0;
        
        private int attackCount;
        
        private float timer;
        private float waitForAnimation = 0.2f;

        private bool cooldown;

        private void Update()
        {
            if (cooldown)
            {
                if (timer < LimitTimer)
                {
                    timer += Time.deltaTime;
                }
                else
                {
                    timer = ResetTimer;
                    cooldown = false;
                }
            }
        }

        public void Attack()
        {
            if (!cooldown)
            {
                attackCount++;
                attackCount = attackCount > MAXAttack ? attackCount = MINAttack : attackCount++;

                switch (attackCount)
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
                
                cooldown = true;
            }
        }

        private IEnumerator Attack1()
        {
            isAttack = true;
            
            animator.SetBool(Combo1, isAttack);

            yield return new WaitForSeconds(waitForAnimation);
            
            isAttack = false;
            
            animator.SetBool(Combo1, isAttack);
        }
        
        private IEnumerator Attack2()
        {
            isAttack = true;
            
            animator.SetBool(Combo2, isAttack);

            yield return new WaitForSeconds(waitForAnimation);
            
            isAttack = false;
            
            animator.SetBool(Combo2, isAttack);
        }
        
        private IEnumerator Attack3()
        {
            isAttack = true;
            
            animator.SetBool(Combo3, isAttack);

            yield return new WaitForSeconds(waitForAnimation);
            
            isAttack = false;
            
            animator.SetBool(Combo3, isAttack);
        }
    }
}
