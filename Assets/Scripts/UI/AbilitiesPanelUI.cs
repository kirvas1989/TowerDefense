using UnityEngine;
using System.Collections.Generic;
using static TowerDefense.Abilities;

namespace TowerDefense
{
    public class AbilitiesPanelUI : MonoBehaviour
    {
        [SerializeField] private FireRangeAbility m_FireRangeAbility;
        [SerializeField] private StopTimeAbility m_StopTimeAbility;
        [SerializeField] private GameObject m_EnergyPanel;
        [SerializeField] private GameObject m_FireRange;
        [SerializeField] private GameObject m_StopTime;
        [SerializeField] private RectTransform m_Rect;
        [SerializeField] private float m_Interval;

        private List<GameObject> m_Abilities;

        private void Awake()
        {
            m_EnergyPanel.SetActive(false);
            m_Abilities = new List<GameObject>();

            var fireLevel = Upgrades.GetUpgradeLevel(m_FireRangeAbility.FireRangeUpgrade);
            if (fireLevel > 0)
            {
                m_FireRange.SetActive(true);
                m_Abilities.Add(m_FireRange);
            }
            else
                m_FireRange.SetActive(false);

            var timeLevel = Upgrades.GetUpgradeLevel(m_StopTimeAbility.StopTimeUpgrade);
            if (timeLevel > 0)
            {
                m_StopTime.SetActive(true);
                m_Abilities.Add(m_StopTime);
            }
            else
                m_StopTime.SetActive(false);
        }

        private void Start()
        {
            if (m_Abilities.Count > 0) m_EnergyPanel.SetActive(true);

            for (int i = 0; i < m_Abilities.Count; i++)
            {
                if (m_Abilities[i].activeSelf)
                {
                    float startPositionX = m_Rect.position.x - m_Interval * (m_Abilities.Count - 1) * 0.5f;

                    m_Abilities[i].transform.position = new Vector2(startPositionX + i * m_Interval, m_Rect.position.y);
                }
            }
        }
    }
}