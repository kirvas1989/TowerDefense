using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu]
    public class ProjectileAsset : ScriptableObject
    {
        public enum DamageType
        {
            Base,
            Magic,
            Elemental
        }

        [SerializeField] public DamageType m_DamageType;

        public Sprite Sprite;

        public float Velocity = 5;
        public float Lifetime = 1;
        public int Damage = 1;

        [SerializeField] public ImpactEffect ImpactEffectPrefab; // ДЗ: Заспавнить объект ImpactEffect в тех координатах, которые имеет Projectile
        [SerializeField] public Bomb BombPrefab;
        [SerializeField] public AudioSource AudioSource;
        [SerializeField] public Sound ShotSound;
        [SerializeField] public Sound HitSound;
        [SerializeField] public bool Freeze = false;
        
        public float FrozenSpeed = 1;

        [SerializeField] public bool DoNotDestroyOnHit = false;
        
    }
}
