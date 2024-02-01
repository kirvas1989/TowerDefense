using UnityEngine;

namespace TowerDefense
{
    public class Bomb : Explosion
    {
        [Header("Damage Options")]
        [SerializeField] private float m_Radius;
        [SerializeField] private int m_Damage;

        private Projectile.DamageType m_DamageType;

        public override void Explode()
        {
            if (m_AudioSource != null && m_AudioSource.clip != null)
                m_AudioSource?.PlayOneShot(m_Clip, volume);

            StartAnimation(true);
            MassAttack(m_Damage, m_DamageType);
            Destroy(gameObject, m_AnimationTime);
        }

        public void SetDamage(int damage, Projectile.DamageType type)
        {
            m_Damage = damage;
            m_DamageType = type;
        }

        private void MassAttack(int damage, Projectile.DamageType type)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, m_Radius);

            if (hits != null && hits.Length > 0)
            {
                foreach (Collider2D hit in hits)
                {
                    Enemy enemy = hit.transform.root.GetComponent<Enemy>();

                    if (enemy != null)
                    {
                        enemy.TakeDamage(damage, type);
                    }
                }
            }
        }
    }
}

