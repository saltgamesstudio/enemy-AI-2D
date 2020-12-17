using UnityEngine;
using Pathfinding;

namespace Enemy
{
    public class EnemyGFX : MonoBehaviour
    {
        public AIPath aiPath;

        // Update is called once per frame
        void Update()
        {
            if (aiPath.desiredVelocity.x >= 0.01f)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (aiPath.desiredVelocity.x <= -0.01f)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}
