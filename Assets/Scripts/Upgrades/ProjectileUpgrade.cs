using UnityEngine;

namespace TowerDefense
{
    public class ProjectileUpgrade : MonoBehaviour
    {
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private UpgradeAsset damageUpgrade;

        private void Start()
        {
            var level = Upgrades.GetUpgradeLevel(damageUpgrade);

            projectilePrefab.UpgradeDamage(level);
        }
    }
}