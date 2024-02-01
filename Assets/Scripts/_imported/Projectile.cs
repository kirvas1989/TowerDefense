using Unity.VisualScripting;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Класс снарядов корабля.
    /// </summary>
    public class Projectile : Entity
    {
        public enum DamageType
        {
            Base = 0,
            Magic = 1,
            Elemental = 3
        }

        [SerializeField] private DamageType m_DamageType;
        
        [SerializeField] private float m_Velocity;
        public float Velocity => m_Velocity;

        [SerializeField] private float m_Lifetime;
        [SerializeField] private int m_Damage;
        [SerializeField] private ImpactEffect m_ImpactEffectPrefab; 
        [SerializeField] private Bomb m_bombPrefab;
        
        [SerializeField] private AudioSource m_AudioSource;
        public AudioSource AudioSource => m_AudioSource;
        [SerializeField] private Sound m_ShotSound = Sound.Arrow;
        [SerializeField] private Sound m_HitSound = Sound.ArrowHit;

        private float m_Timer;
        private float m_FreezeRate;
        private bool m_Freeze = false;
        private bool m_DoNotDestroyOnHit = false;

        #region Unity Events

        /// <summary>
        /// Код на попадание выстрелом по Destructible.
        /// </summary>

        private void Start()
        {
            m_ShotSound.Play();
        }

        private void Update()
        {
            float stepLength = Time.deltaTime * m_Velocity;
            Vector2 step = transform.up * stepLength;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength);

            /// <summary>
            /// Код при попадании выстрелом по Destructible.
            /// </summary>
            if (hit)
            {
                OnEnemyHit(hit);
            }

            m_Timer += Time.deltaTime;

            if (m_Timer > m_Lifetime)
                Destroy(gameObject);

            transform.position += new Vector3(step.x, step.y, 0);
        }

        private void OnEnemyHit(RaycastHit2D hit)
        {
            var enemy = hit.collider.transform.root.GetComponent<Enemy>();

            if (enemy != null)
            {
                m_HitSound.Play();
                
                if (m_Freeze == true)
                {
                    var ship = enemy.GetComponent<SpaceShip>();
                    var sr = enemy.GetComponentInChildren<SpriteRenderer>();

                    if (ship && sr)
                    {
                        ship.SlowDown(m_FreezeRate);
                        sr.color = Color.cyan;
                    }
                }

                if (m_DoNotDestroyOnHit)
                {
                    if (enemy.IsDamaged == false)
                        enemy.TakeDamage(m_Damage, m_DamageType);
                }
                else
                {
                    OnProjectileLifeEnd(hit.collider, hit.point);
                    enemy.TakeDamage(m_Damage, m_DamageType);
                }
            }
        }

        private void OnDestructibleHit(RaycastHit2D hit)
        {
            Destructible dest = hit.collider.transform.root.GetComponent<Destructible>();

            if (dest != null)
            {
                if (m_Freeze == true)
                {
                    var ship = dest.GetComponent<SpaceShip>();
                    var sr = dest.GetComponentInChildren<SpriteRenderer>();

                    if (ship && sr)
                    {
                        ship.SlowDown(m_FreezeRate);
                        sr.color = Color.cyan;
                    }
                }

                dest.ApplyDamage(m_Damage);

                if (m_DoNotDestroyOnHit == false)
                {
                    OnProjectileLifeEnd(hit.collider, hit.point);
                }
            }
        }
        #endregion

        /// <summary>
        /// Код при промахе снарярдом.
        /// </summary>
        private void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
        {
            // Сюда дополнительно можно добавить ачивки и т.д.

            if (m_bombPrefab != null)
            {
                Bomb bomb = Instantiate(m_bombPrefab, transform.position,
                            Quaternion.Euler(0, 0, Random.Range(0, 360)));


                bomb.SetDamage(m_Damage, m_DamageType);
                bomb.Explode();
            }

            if (m_ImpactEffectPrefab != null)
            {
                ImpactEffect impact = Instantiate(m_ImpactEffectPrefab, transform.position,
                                      Quaternion.Euler(0f, 0f, Random.Range(0, 360)));

                impact.GetComponent<AnimateSpriteFrames>().StartAnimation(true);
                impact.GetComponent<AudioSource>().Play();
            }

            Destroy(gameObject);
        }

        #region Защита от попадания в себя

        private Destructible m_Parent;
        public void SetParentShooter(Destructible parent)
        {
            m_Parent = parent;
        }

        #endregion

        #region PublicAPI

        public void SetTarget(Destructible target)
        {

        }

        public void Use(ProjectileAsset asset)
        {
            var sr = transform.Find("VisualModel").GetComponent<SpriteRenderer>();
            sr.sprite = asset.Sprite;
            m_Velocity = asset.Velocity;
            m_Lifetime = asset.Lifetime;    
            m_Damage = asset.Damage;
            m_ImpactEffectPrefab = asset.ImpactEffectPrefab;
            m_bombPrefab = asset.BombPrefab;
            m_AudioSource = asset.AudioSource;
            m_ShotSound = asset.ShotSound;
            m_HitSound = asset.HitSound;
            m_FreezeRate = asset.FrozenSpeed;
            m_Freeze = asset.Freeze;
            m_DoNotDestroyOnHit = asset.DoNotDestroyOnHit;

            if (asset.m_DamageType == ProjectileAsset.DamageType.Base)
                m_DamageType = DamageType.Base;

            if (asset.m_DamageType == ProjectileAsset.DamageType.Magic)
                m_DamageType = DamageType.Magic;

            if (asset.m_DamageType == ProjectileAsset.DamageType.Elemental)
                m_DamageType = DamageType.Elemental;
        }

        public int UpgradeDamage(int bonusDamage)
        {
            m_Damage += bonusDamage;
            return m_Damage;
        }

        #endregion
    }
}
