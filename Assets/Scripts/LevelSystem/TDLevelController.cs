using UnityEngine;

namespace TowerDefense
{
    public class TDLevelController : LevelController
    {
        private new void Start()
        {
            base.Start();
            TDPlayer.Instance.OnPlayerDead += () =>
            {
                StopLevelActivity();
                LevelResultController.Instance.Show(false);

            };

            m_ReferenceTime += Time.time;

            m_EventLevelCompleted.AddListener(() =>
            {
                StopLevelActivity();
                
                if (m_ReferenceTime <= Time.time)
                {
                    m_LevelScore -= 1;
                }

                MapCompletion.SaveEpisodeResult(m_LevelScore);
            });

            void LivesScoreChange(int _)
            {
                m_LevelScore -= 1;
                TDPlayer.OnLivesUpdate -= LivesScoreChange;
            }

            TDPlayer.OnLivesUpdate += LivesScoreChange;
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
        }
    }
}