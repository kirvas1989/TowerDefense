using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// �����, �������� �� ��������.
    /// </summary>
    public enum TurretMode
    {
        Primary,
        Secondary,
        Auto
    }

    [CreateAssetMenu]
    public sealed class TurretProperties : ScriptableObject // SEALED - ������, ��� �� ������ ������ �������������.
    {
        [SerializeField] private TurretMode m_Mode;
        public TurretMode Mode => m_Mode;


        [SerializeField] private Projectile m_ProjectilePrefab;
        public Projectile ProjectilePrefab => m_ProjectilePrefab;


        [SerializeField] private ProjectileAsset m_ProjectileAsset;
        public ProjectileAsset ProjectileAsset => m_ProjectileAsset;
        

        [SerializeField] private float m_RateOfFire;
        public float RateOfFire => m_RateOfFire;


        [SerializeField] private int m_EnergyUsage;
        public int EnergyUsage => m_EnergyUsage;


        [SerializeField] private int m_AmmoUsage;
        public int AmmoUsage => m_AmmoUsage;


        // TODO: ��������� �������������� ��������������� � ProjectileAsset
        [SerializeField] private AudioClip m_LaunchSFX; 
        public AudioClip LaunchSFX => m_LaunchSFX;


        public float Velocity => m_ProjectilePrefab.Velocity;
    }
}
