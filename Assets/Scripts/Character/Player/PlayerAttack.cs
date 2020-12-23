using System.Collections;
using UnityEngine;

namespace Character.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        public Animator animator;
        
        [HideInInspector] public bool isAttack;

        private static readonly int Combo1 = Animator.StringToHash("Attack1");
        private static readonly int Combo2 = Animator.StringToHash("Attack2");
        private static readonly int Combo3 = Animator.StringToHash("Attack3");

        private int attackCount;

        public void Attack()
        {
            if (Input.GetMouseButtonDown(0) && !isAttack)
            {
                attackCount++;
                attackCount = attackCount > 3 ? attackCount = 1 : attackCount++;

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

                Debug.Log("Attack Count : " +attackCount);
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
