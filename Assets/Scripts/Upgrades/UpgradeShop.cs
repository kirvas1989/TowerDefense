using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class UpgradeShop : MonoBehaviour
    {
        [SerializeField] private Text moneyText;
        [SerializeField] private int money;
        [SerializeField] private BuyUpgrade[] sales;

        private void Start()
        {
            foreach (var slot in sales)
            {
                slot.Initialize();
                slot.transform.GetComponentInChildren<Button>().
                     onClick.AddListener(UpdateMoney);               
                UpdateMoney();
            }
        }

        public void UpdateMoney() 
        {
            money = MapCompletion.Instance.TotalScore;
            money -= Upgrades.GetTotalCost();
            moneyText.text = money.ToString();

            foreach (var slot in sales)
            {
                slot.CheckCost(money);
            }
        }

        private void OnDestroy()
        {
            foreach (var slot in sales)
            {
                slot.transform.GetComponentInChildren<Button>().
                     onClick.RemoveListener(UpdateMoney);
            }
        }
    }
}