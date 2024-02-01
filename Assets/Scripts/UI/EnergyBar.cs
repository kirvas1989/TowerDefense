using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class EnergyBar : MonoBehaviour
    {
        [SerializeField] private Image fillBar;

        private void Start()
        {
            fillBar.fillAmount = 1;
        }

        private void FixedUpdate()
        {
            if (fillBar != null && TDPlayer.Instance.CurrentEnergy < TDPlayer.Instance.TotalEnergy)
            {
                fillBar.fillAmount = (float) TDPlayer.Instance.CurrentEnergy / TDPlayer.Instance.TotalEnergy;
            }  
        }
    }
}