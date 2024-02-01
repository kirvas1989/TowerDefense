using UnityEngine;
using UnityEngine.UI;
using static TowerDefense.Abilities;

namespace TowerDefense
{
    public class FireRangeAbilityUi : MonoBehaviour
    {
        [SerializeField] private FireRangeAbility m_FireRangeAbility;
        [SerializeField] private Button m_FireRangeButton;
        [SerializeField] private Text m_EnergyCostText;
        [SerializeField] private GameObject m_TargetCirclePrefab;

        private bool isActivated;

        private void Awake()
        {
            var level = Upgrades.GetUpgradeLevel(m_FireRangeAbility.FireRangeUpgrade);
            var rect = m_TargetCirclePrefab.GetComponent<RectTransform>();
            float scaledSize = (float) (1 + level) / 2;
            rect.localScale = new Vector3(scaledSize, scaledSize, 1f);
            m_TargetCirclePrefab.SetActive(false);
        }

        private void Start()
        {
            m_EnergyCostText.text = m_FireRangeAbility.Cost.ToString();
            FireRangeAbility.OnFireRange += OnFireRangeEvent;
        }

        private void Update()
        {
            if (m_FireRangeAbility.Cost > TDPlayer.Instance.CurrentEnergy)
            {
                m_EnergyCostText.color = Color.red;
                m_FireRangeButton.interactable = false;
            }
            else
            {
                m_EnergyCostText.color = Color.white;
                m_FireRangeButton.interactable = true;
            }

            if (isActivated)
            {
                Vector3 mousePos = new Vector3();
                mousePos = Input.mousePosition;
                m_TargetCirclePrefab.transform.position = mousePos;
            }
        }

        private void OnDestroy()
        {
            FireRangeAbility.OnFireRange -= OnFireRangeEvent;
        }

        private void OnFireRangeEvent(bool active)
        {
            isActivated = active;
            m_TargetCirclePrefab.SetActive(active);
        }
    }
}