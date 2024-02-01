using UnityEngine;

namespace TowerDefense
{
    public class TurretUpgrade : MonoBehaviour
    {
        [SerializeField] private Turret m_Turret;
        [SerializeField] private UpgradeAsset speedUpgrade;
        [SerializeField] private float speedBonus = 0.1f;

        private void Start()
        {
            var level = Upgrades.GetUpgradeLevel(speedUpgrade);

            m_Turret.UpgradeFireRate(speedBonus * level);
        }
    }
}
