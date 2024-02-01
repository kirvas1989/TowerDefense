using UnityEngine;

namespace TowerDefense
{
    public class EnemySpawner : Spawner
    {
        [SerializeField] private Enemy m_EnemyPrefab;
        [SerializeField] private EnemyAsset[] m_EnemyAssets;
        [SerializeField] private Path m_Path;

        protected override GameObject GenerateSpawnedEntity()
        {
            var enemy = Instantiate(m_EnemyPrefab);

            enemy.Use(m_EnemyAssets[Random.Range(0, m_EnemyAssets.Length)]);
            enemy.GetComponent<TDPatrolController>().SetPath(m_Path);

            return enemy.gameObject;
        }
    }
}

