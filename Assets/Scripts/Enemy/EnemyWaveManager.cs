using System;
using UnityEngine;

namespace TowerDefense
{
    public class EnemyWaveManager : MonoBehaviour
    {
        public static event Action<Enemy> OnEnemySpawn;

        [SerializeField] private Enemy m_EnemyPrefab;
        [SerializeField] private Path[] paths;
        [SerializeField] private EnemyWave currentWave;

        public event Action OnAllWavesDead;
        public event Action OnAllEnemiesDead;

        private int activeEnemyCount = 0;  

        private void Start()
        {
            currentWave.Prepare(SpawnEnemies);
        }

        private void SpawnEnemies()
        {
            foreach ((EnemyAsset asset, int count, int pathIndex) in currentWave.EnumerateSquad())
            {
                if (pathIndex < paths.Length)
                {
                    for (int i = 0; i < count; i++)
                    {
                        var enemy = Instantiate(m_EnemyPrefab, paths[pathIndex].StartArea.GetRandomInsideZone(), Quaternion.identity);
                        enemy.OnEnd += RecordEnemyDead;
                        enemy.Use(asset);
                        enemy.GetComponent<TDPatrolController>().SetPath(paths[pathIndex]);
                        activeEnemyCount += 1;
                        OnEnemySpawn?.Invoke(enemy);
                    }
                }
                else
                {
                    Debug.LogWarning($"Invalid pathIndex in {name}");
                }
            }

            currentWave = currentWave.PrepareNext(SpawnEnemies);
        }

        private void RecordEnemyDead()
        { 
            if (--activeEnemyCount == 0) // ��������� ���������� �� 1 � ���������, �� ����� �� ��� 0.
            {
                if (currentWave)
                {
                    ForceNextWave();
                }
                else
                {
                    OnAllEnemiesDead?.Invoke();
                }
            }
        }

        public void ForceNextWave()
        {
            if (currentWave)
            {
                TDPlayer.Instance.ChangeGold((int)currentWave.GetRemainingTime());
                SpawnEnemies();
            }
            else
            {
                OnAllWavesDead?.Invoke();
            }
        }

        // ���������!!! (����� �� ���������).
        //private void OnDestroy()
        //{
        //    currentWave.OnWaveReady -= SpawnEnemies; 
        //}
    }
}