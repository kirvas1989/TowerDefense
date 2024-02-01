using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace TowerDefense
{
    public class Pause : MonoBehaviour
    {
        [SerializeField] private Button m_PauseButton;
        [SerializeField] private Button m_NextWaveButton;
        [SerializeField] private GameObject m_PausePanel;
        
        private void Awake()
        {
            if(m_PausePanel)
                m_PausePanel.SetActive(false);
        }

        public void PauseGame()
        {
            m_PauseButton.interactable = false; 
            m_NextWaveButton.interactable = false;
            m_PausePanel.SetActive(true);      
            StopLevelActivity();
        }

        public void Continue()
        {
            m_PauseButton.interactable = true;
            m_NextWaveButton.interactable= true;
            m_PausePanel.SetActive(false);
            StartLevelActivity();
        }

        public void Restart()
        {
            LevelSequenceController.Instance.RestartLevel();
        }

        public void MainMenu()
        {
            SceneManager.LoadScene(0);
        }
    
        public void LoadLevelMap()
        {
            SceneManager.LoadScene(1);
        }

        private void StartLevelActivity()
        {
            foreach (var enemy in FindObjectsOfType<Enemy>())
            {
                enemy.GetComponent<SpaceShip>().enabled = true;
                enemy.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }

            foreach (var dest in FindObjectsOfType<Destructible>())
                dest.GetComponent<Rigidbody2D>().velocity = dest.StartVelocity;

            void EnableAll<T>() where T : MonoBehaviour
            {
                foreach (var obj in FindObjectsOfType<T>())
                {
                    obj.enabled = true;
                }
            }

            EnableAll<Spawner>();
            EnableAll<EnemyWaveManager>();
            EnableAll<EnemyWave>();
            EnableAll<Projectile>();
            EnableAll<Tower>();
            EnableAll<BuildSite>();
            EnableAll<NextWaveGUI>();
            EnableAll<StopTimeAbilityUI>();
            EnableAll<TDPlayer>(); 
            EnableAll<TDLevelController>(); 
        }

        private void StopLevelActivity()
        {
            foreach (var enemy in FindObjectsOfType<Enemy>())
            {
                enemy.GetComponent<SpaceShip>().enabled = false;
                enemy.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                enemy.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            }

            void DisableAll<T>() where T : MonoBehaviour
            {
                foreach (var obj in FindObjectsOfType<T>())
                {
                    obj.enabled = false;
                }
            }

            DisableAll<Spawner>();
            DisableAll<EnemyWaveManager>();
            DisableAll<EnemyWave>();
            DisableAll<Projectile>();
            DisableAll<Tower>();
            DisableAll<BuildSite>();
            DisableAll<NextWaveGUI>();
            DisableAll<StopTimeAbilityUI>();
            DisableAll<TDPlayer>();
            DisableAll<TDLevelController>();
        }      
    }
}