using UnityEngine;

namespace TowerDefense
{
    public abstract class Spawner : MonoBehaviour
    {
        public enum SpawnMode
        {
            Start,
            Loop
        }

        protected abstract GameObject GenerateSpawnedEntity();

        [SerializeField] private CircleArea m_Area;
        [SerializeField] private SpawnMode m_SpawnMode;
        [SerializeField] private int m_NumSpawns;
        [SerializeField] private float m_RespawnTime;
        [SerializeField] private bool m_AutoDestroy;
        [SerializeField] private float m_DestroyTime;

        private float m_Timer;

        private void Start()
        {
            if (m_SpawnMode == SpawnMode.Start)
            {
                SpawnEntities();
            }

            m_Timer = m_RespawnTime;
        }

        private void Update()
        {
            if (m_Timer > 0)
                m_Timer -= Time.deltaTime;

            if (m_SpawnMode == SpawnMode.Loop && m_Timer < 0)
            {
                SpawnEntities();
                m_Timer = m_RespawnTime;
            }
        }

        private void SpawnEntities()
        {
            for (int i = 0; i < m_NumSpawns; i++)
            {
                var entity = GenerateSpawnedEntity();
                entity.transform.position = m_Area.GetRandomInsideZone();

                if (m_AutoDestroy == true && m_DestroyTime > 0)
                    Destroy(entity, m_DestroyTime);
            }
        }
    }
}
