using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class TowerSellControl : MonoBehaviour
    {
        [SerializeField] private Tower m_Tower;
        [SerializeField] private Transform m_BuildSite;
        [SerializeField] private GameObject visualPanel;
        [SerializeField] private Text m_Text;

        const int costMultiplier = 2;

        public static int SellCost(Tower tower)
        {
            int sellCost = tower.Cost / costMultiplier;
            return sellCost;
        }

        #region UnityEvents

        private void Awake()
        {
            visualPanel.SetActive(true);
        }

        private void Start()
        {               
            BuildSite.OnClickEvent += SetBuildSite;
            m_Text.color = Color.green;
        }

        private void OnDestroy()
        {
            BuildSite.OnClickEvent -= SetBuildSite;
        }

        #endregion

        #region PublicAPI

        public void SetBuildSite(Transform buildSite)
        {
            m_BuildSite = buildSite;

            Tower[] towers = FindObjectsOfType<Tower>();

            if (towers.Length > 0 && m_BuildSite != null && m_Text != null)
            {
                foreach (var tower in towers)
                {
                    if (tower.transform.position == m_BuildSite.position)
                    {
                        m_Tower = tower;
                        m_Text.text = SellCost(m_Tower).ToString();
                        break;
                    }
                }
            }
        }
        
        public void Sell()
        {
            TDPlayer.Instance.TrySell(m_BuildSite, m_Tower);
            BuildSite.HideControls();
        }

        #endregion
    }
}
