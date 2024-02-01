using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class TowerUpgrade : MonoBehaviour
    {
        [SerializeField] private UpgradeAsset towerUpgrade;
        [SerializeField] private Button button;
        [SerializeField] private GameObject goldImage;
        [SerializeField] private GameObject goldPanel;
        [SerializeField] private GameObject infoPanel;
        [SerializeField] private GameObject lockImage;

        private bool isLocked = false;
        public bool IsLocked => isLocked;

        private void Awake()
        {          
            if (towerUpgrade != null)
            {
                var level = Upgrades.GetUpgradeLevel(towerUpgrade);

                if (level == 0)
                {
                    isLocked = true;
                    button.interactable = false;
                    goldPanel.SetActive(false);
                    goldImage.SetActive(false);
                    infoPanel.SetActive(true);
                    lockImage.SetActive(true);
                }
                else
                {
                    isLocked = false;
                    button.interactable = true;
                    goldPanel.SetActive(true);
                    goldImage.SetActive(true);
                    infoPanel.SetActive(false);
                    lockImage.SetActive(false);
                }
            }           
        }
    }
}