using System;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class BuyUpgrade : MonoBehaviour
    {
        [SerializeField] private UpgradeAsset asset;
        [SerializeField] private Image upgradeIcon;
        [SerializeField] private Text levelText;
        [SerializeField] private Text costText;
        [SerializeField] private Text buyText;
        [SerializeField] private Button buyButton;

        private int costNum = 0;

        private void Awake() 
        {
            Initialize();
        }

        public void Initialize()
        {
            var savedLevel = Upgrades.GetUpgradeLevel(asset);

            upgradeIcon.sprite = asset.sprite;

            if (savedLevel >= asset.costByLevel.Length)
            {
                levelText.text = "MAX";
                buyButton.interactable = false;
                buyButton.transform.Find("StarIcon").gameObject.SetActive(false);
                buyButton.transform.Find("BuyText").gameObject.SetActive(false);
                buyButton.transform.Find("CostText").gameObject.SetActive(false);
                buyButton.transform.Find("InfoText").gameObject.SetActive(true);                
            }
            else
            {
                levelText.text = (savedLevel + 1).ToString(); //!!!
                costNum = asset.costByLevel[savedLevel];
                costText.text = costNum.ToString();         
            }            
        }

        public void Buy()
        {
            Upgrades.BuyUpgrade(asset);
            Initialize();
        }

        public void CheckCost(int money)
        {
            if (money >= costNum)
            {
                buyText.text = "Buy:";
            }
            else
            {
                buyButton.interactable = false;
                buyText.text = "Need:";
            }
        }
    }
}