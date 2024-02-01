using UnityEngine;

namespace TowerDefense
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private TurretMode m_Mode;
        public TurretMode Mode => m_Mode;

        [SerializeField] private TurretProperties m_TurretProperties;

        private float m_RefireTimer;

        private SpaceShip m_Ship;

        #region Unity Events
        private void Start()
        {
            m_Ship = transform.root.GetComponent<SpaceShip>();
        }

        private void Update()
        {
            if (m_RefireTimer > 0)
                m_RefireTimer -= Time.deltaTime;
            else if (m_Mode == TurretMode.Auto)
                Fire();
        }
        #endregion

        #region Public API
        public void Fire()
        {
            if (m_TurretProperties == null) return;

            if (m_RefireTimer > 0) return;

            if (m_Ship)
            {
                if (m_Ship.DrawEnergy(m_TurretProperties.EnergyUsage) == false) // ���������, ������� �� ������� ��� ���������.
                    return;

                if (m_Ship.DrawAmmo(m_TurretProperties.AmmoUsage) == false) // ���������, ������� �� ��������� ��� ���������.
                    return;
            }

            Projectile projectile = Instantiate(m_TurretProperties.ProjectilePrefab).GetComponentInParent<Projectile>();
            projectile.transform.position = transform.position;
            projectile.transform.up = transform.up;          
            projectile.SetParentShooter(m_Ship);

            if (m_TurretProperties.ProjectileAsset != null)
                projectile.Use(m_TurretProperties.ProjectileAsset);

            if (projectile.AudioSource != null && projectile.AudioSource.clip != null)
                projectile.AudioSource.PlayOneShot(m_TurretProperties.LaunchSFX);

            m_RefireTimer = m_TurretProperties.RateOfFire;    
        }

        public void Use(TurretProperties asset)
        {
            m_TurretProperties = asset;
        }
        
        public float ProjectileVelocity()
        {
            float velocity = m_TurretProperties.ProjectilePrefab.Velocity;

            return velocity;
        }

        public float UpgradeFireRate(float speedBonus)
        {
            m_RefireTimer -= speedBonus;
            return m_RefireTimer;
        }

        #endregion
    }
}
