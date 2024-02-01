using UnityEngine;

namespace TowerDefense
{
    public class StandUp : MonoBehaviour
    {
        private Rigidbody2D rig;
        private SpriteRenderer sr;

        float m_Timer = 1;

        private void Start()
        {
            rig = transform.root.GetComponent<Rigidbody2D>();
            sr = GetComponent<SpriteRenderer>();
        }

        private void LateUpdate()
        {
            transform.up = Vector2.up;
            var xMotion = rig.velocity.x;

            if (m_Timer > 0)
            {
                m_Timer -= Time.deltaTime;
            }
            else
            {
                if (xMotion > 0.01f) sr.flipX = false;
                else if (xMotion < 0.01f) sr.flipX = true;
                m_Timer = 1;
            }
        }
    }
}