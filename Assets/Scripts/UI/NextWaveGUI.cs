using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class NextWaveGUI : MonoBehaviour
    {
        [SerializeField] private Text bonusAmount;
        [SerializeField] private Button nextWaveButton;
        [SerializeField] private Image fillBar;
        
        private EnemyWaveManager manager;
        private float timeToNextWave;
        private float fillTime;
        private bool barIsActive;

        private void Start()
        {
            manager = FindObjectOfType<EnemyWaveManager>();
            manager.OnAllWavesDead += DisableGUI;
            manager.OnAllEnemiesDead += DisableGUI;
            TDPlayer.Instance.OnPlayerDead += DisableGUI;
            EnemyWave.OnLastWave += DisableGUI;
            EnemyWave.OnWavePrepare += (float time) =>
            {
                timeToNextWave = time;
                barIsActive = true;
                fillTime = timeToNextWave;
            };
        }

        private void LateUpdate()
        {
            var bonus = (int)timeToNextWave;
            if (bonus < 0) bonus = 0;                 
            bonusAmount.text = bonus.ToString();
            
            if (barIsActive)
            {
                fillBar.fillAmount = Mathf.Lerp(1, timeToNextWave / fillTime, fillTime);
            }
            else
            {
                fillBar.fillAmount = 0;
                bonusAmount.text = "0";
                return;
            }

            timeToNextWave -= Time.deltaTime;
        }

        private void OnDestroy()
        {
            manager.OnAllWavesDead -= DisableGUI;
            manager.OnAllEnemiesDead -= DisableGUI;
            TDPlayer.Instance.OnPlayerDead -= DisableGUI;
            EnemyWave.OnLastWave -= DisableGUI;
            EnemyWave.OnWavePrepare -= (float time) =>
            {
                timeToNextWave = time;
                barIsActive = true;
                fillTime = timeToNextWave;
            };
        }

        private void DisableGUI()
        {
            barIsActive = false;
            if (nextWaveButton)
                nextWaveButton.interactable = false;
        }

        public void CallWave()
        {
            manager.ForceNextWave();
        }
    }
}