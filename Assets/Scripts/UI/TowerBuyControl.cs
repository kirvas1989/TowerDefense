using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class TowerBuyControl : MonoBehaviour
    {
        [SerializeField] private GameObject visualPanel;
        [SerializeField] private TowerAsset m_Asset;
        [SerializeField] private Text m_Text;
        [SerializeField] private Button m_Button;
        [SerializeField] private Transform m_BuildSite;
        [SerializeField] private Sound m_BuildSound;

        private TowerUpgrade upgrade;

        private void Awake()
        {
            visualPanel.SetActive(true);
        }

        private void Start()
        {
            TDPlayer.GoldUpdateSubscribe(GoldStatusCheck);
            upgrade = GetComponent<TowerUpgrade>();
            m_Text.text = m_Asset.GoldCost.ToString();
            m_Button.GetComponent<Image>().sprite = m_Asset.GUISprite;
            GoldStatusCheck(TDPlayer.Instance.Gold);
        }

        private void OnDestroy()
        {
            TDPlayer.RemoveGoldUpdateSubscribe(GoldStatusCheck);
        }

        private void GoldStatusCheck(int gold)
        {
            if (gold < m_Asset.GoldCost)
            {
                m_Button.interactable = false;
                m_Text.color = Color.red;
            }
            else
            {
                if (upgrade)
                {
                    if (upgrade.IsLocked) m_Button.interactable = false;
                    else m_Button.interactable = true;
                }
                else m_Button.interactable = true;
                m_Text.color = Color.white;
            }
        }

        public void Buy()
        {
            m_BuildSound.Play();
            TDPlayer.Instance.TryBuild(m_Asset, m_BuildSite);
            BuildSite.HideControls();
        }

        public void SetBuildSite(Transform value)
        {
            m_BuildSite = value;
        }

        public void SetTowerAsset(TowerAsset asset )
        {
            m_Asset = asset;
        }

        //[SerializeField] private GameObject visualPanel;
        //[SerializeField] private TowerAsset m_Asset;
        //[SerializeField] private Text m_Text;
        //[SerializeField] private Button m_Button;
        //[SerializeField] private Transform m_BuildSite;

        //private TowerUpgrade upgrade;

        //private void Awake()
        //{
        //    visualPanel.SetActive(true);
        //}

        //private void Start()
        //{
        //    TDPlayer.GoldUpdateSubscribe(GoldStatusCheck);
        //    upgrade = GetComponent<TowerUpgrade>();
        //    m_Text.text = m_Asset.GoldCost.ToString();
        //    m_Button.GetComponent<Image>().sprite = m_Asset.GUISprite;
        //    GoldStatusCheck(TDPlayer.Instance.Gold);
        //}

        //private void OnDestroy()
        //{
        //    TDPlayer.RemoveGoldUpdateSubscribe(GoldStatusCheck);
        //}

        //private void GoldStatusCheck(int gold)
        //{
        //    if (gold < m_Asset.GoldCost)
        //    {
        //        m_Button.interactable = false;
        //        m_Text.color = Color.red;
        //    }
        //    else 
        //    {
        //        if (upgrade) 
        //        {
        //            if (upgrade.IsLocked) m_Button.interactable = false;
        //            else m_Button.interactable = true;
        //        }
        //        else m_Button.interactable = true;               
        //        m_Text.color = Color.white;
        //    }
        //}

        //public void Buy()
        //{
        //    TDPlayer.Instance.TryBuild(m_Asset, m_BuildSite);
        //    BuildSite.HideControls();
        //}

        //public void SetBuildSite(Transform value)
        //{
        //    m_BuildSite = value;
        //}
    }
}