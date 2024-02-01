using System;
using UnityEngine;

namespace TowerDefense
{
    public class Upgrades : SingletonBase<Upgrades>
    {
        public const string Filename = "upgrades.dat";
    
        [Serializable]
        private class UpgradeSave
        {
            public UpgradeAsset asset;
            public int level = 0;
        }

        [SerializeField] private UpgradeSave[] save;

        private new void Awake()
        {
            base.Awake();

            Saver<UpgradeSave[]>.TryLoad(Filename, ref save);
        }

        #region Public API

        public static void BuyUpgrade(UpgradeAsset asset)
        {
            foreach (var upgrade in Instance.save)
            {
                if (upgrade.asset == asset)
                {
                    upgrade.level += 1;
                    Saver<UpgradeSave[]>.Save(Filename, Instance.save);
                }
            }
        }

        public static int GetUpgradeLevel(UpgradeAsset asset)
        {
            if (Instance != null && Instance.save != null)
            {
                foreach (var upgrade in Instance.save)
                {
                    if (upgrade.asset == asset)
                    {
                        return upgrade.level;
                    }
                }
            }

            return 0;
        }

        public static int GetTotalCost()
        {
            int result = 0;

            if (Instance != null && Instance.save != null)
            {
                foreach (var upgrade in Instance.save)
                {
                    for (int i = 0; i < upgrade.level; i++)
                    {
                        result += upgrade.asset.costByLevel[i];
                    }
                }
            }

            return result;
        }
        #endregion
    }
}
