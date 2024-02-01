using UnityEngine;
using System;

namespace TowerDefense
{
    public class TDPlayer : Player
    {
        public static new TDPlayer Instance => Player.Instance as TDPlayer;

        #region UnityEvents

        private void Start()
        {
            var level = Upgrades.GetUpgradeLevel(healthUpgrade);
            TakeDamage(-level * healthBonus);
            m_CurrentEnergy = m_TotalEnergy;
        }

        private void Update()
        {
            if (m_CurrentEnergy < 0) m_CurrentEnergy = 0;
            if (m_CurrentEnergy > m_TotalEnergy) m_CurrentEnergy = m_TotalEnergy;
            if (m_CurrentEnergy < m_TotalEnergy)
            {
                timer += m_EnergyPerSecond * Time.deltaTime;
                m_CurrentEnergy = (int) timer;
            }
        }

        #endregion

        #region Energy

        [SerializeField] private int m_TotalEnergy = 100;
        public int TotalEnergy => m_TotalEnergy;

        private int m_CurrentEnergy;
        public int CurrentEnergy => m_CurrentEnergy;

        [SerializeField] private float m_EnergyPerSecond = 1;
        private float timer;

        //private static event Action<int> OnEnergyUpdate;

        //public static void EnergyUpdateSubscribe(Action<int> action)
        //{
        //    OnEnergyUpdate += action;
        //    action(Instance.m_CurrentEnergy);
        //}

        //public static void RemoveEnergyUpdateSubscribe(Action<int> action)
        //{
        //    OnEnergyUpdate -= action;
        //}

        public void ChangeEnergy(int energy)
        {
            m_CurrentEnergy += energy;
            timer = m_CurrentEnergy;
        }

        #endregion

        #region Gold

        [SerializeField] private SoundRandomizer m_Sound;

        private static event Action<int> OnGoldUpdate;
        public static void GoldUpdateSubscribe(Action<int> action)
        {
            OnGoldUpdate += action;
            action(Instance.m_Gold);
        }

        public static void RemoveGoldUpdateSubscribe(Action<int> action)
        {
            OnGoldUpdate -= action;
        }

        [SerializeField] private int m_Gold = 0;
        public int Gold => m_Gold;
        public void ChangeGold(int num)
        {
            m_Gold += num;
            OnGoldUpdate(m_Gold);   
            if (m_Sound) m_Sound.Play();
        }

        #endregion

        #region Lives

        [SerializeField] private UpgradeAsset healthUpgrade;
        [SerializeField] private int healthBonus;

        public static event Action<int> OnLivesUpdate;
        public static void LivesUpdateSubscribe(Action<int> action)
        {
            OnLivesUpdate += action;
            action(Instance.NumLives);
        }

        public static void RemoveLivesUpdateSubscribe(Action<int> action)
        {
            OnLivesUpdate -= action;
        }

        public void ReduceLives(int num)
        {
            TakeDamage(num);
            OnLivesUpdate(NumLives);
        }

        #endregion

        #region Towers

        [SerializeField] private Tower m_TowerPrefab;

        private void CleanBuiltSite(Transform buildSite)
        {
            Tower[] towers = FindObjectsOfType<Tower>();

            if (towers != null && towers.Length > 0)
            {
                foreach (var tower in towers)
                {
                    if (tower.transform.position == buildSite.position)
                    {
                        Destroy(tower.gameObject);
                    }
                }
            }
        }

        public void TryBuild(TowerAsset towerAsset, Transform buildSite)
        {
            CleanBuiltSite(buildSite);

            var tower = Instantiate(m_TowerPrefab, buildSite.position, Quaternion.identity);
            tower.GetComponentInChildren<Turret>().Use(towerAsset.TurretPropertiesAsset);            
            tower.GetComponentInChildren<BuildSite>().SetBuildableTowers(towerAsset.UpgradesTo);
            tower.SetCost(towerAsset.GoldCost);

            var sr = tower.GetComponentInChildren<SpriteRenderer>();
            sr.sprite = towerAsset.Sprite;
            sr.color = towerAsset.Color;

            buildSite.GetComponent<BuildSite>().AllowToBuild(false);
            buildSite.GetComponentInChildren<SpriteRenderer>().enabled = false;

            ChangeGold(-towerAsset.GoldCost);
        }

        public void TrySell(Transform buildSite, Tower tower)
        {
            ChangeGold(TowerSellControl.SellCost(tower));
            
            buildSite.GetComponent<BuildSite>().AllowToBuild(true);
            buildSite.GetComponentInChildren<SpriteRenderer>().enabled = true;
            
            tower.GetComponentInChildren<Turret>().enabled = false;
            Destroy(tower.gameObject);
        }

        #endregion
    }
}
