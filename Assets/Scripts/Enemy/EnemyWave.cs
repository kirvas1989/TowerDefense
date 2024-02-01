using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class EnemyWave : MonoBehaviour
    {
        [Serializable]
        private class Squad
        {
            public EnemyAsset asset;
            public int count;
        }

        [Serializable]
        private class PathGroup
        {
            public Squad[] squads;
        }

        public float GetRemainingTime()
        {
            return prepareTime - Time.time; 
        }

        private void Awake()
        {
            enabled = false;
        }

        private void Update()
        {
            if (Time.time >= prepareTime)
            {
                enabled = false;
                OnWaveReady?.Invoke();
            }
        }

        [SerializeField] private float prepareTime = 10f;

        public static Action<float> OnWavePrepare;
        private event Action OnWaveReady;       
        
        public void Prepare(Action spawnEnemies)
        {
            OnWavePrepare?.Invoke(prepareTime);
            prepareTime += Time.time;
            enabled = true;
            OnWaveReady += spawnEnemies;
        }

        [SerializeField] private PathGroup[] groups;
        public IEnumerable<(EnemyAsset asset, int count, int pathIndex)> EnumerateSquad()
        {
            for (int i = 0; i < groups.Length; i++)
            {
                foreach (var squad in groups[i].squads)
                {
                    yield return (squad.asset, squad.count, i);
                }                
            }    
        }

        [SerializeField] private EnemyWave next;
        public static event Action OnLastWave;
        public EnemyWave PrepareNext(Action spawnEnemies)
        {
            if (next == null) OnLastWave?.Invoke();

            OnWaveReady -= spawnEnemies;       
            if (next) next.Prepare(spawnEnemies);
            return next;
        } 
    }
}
