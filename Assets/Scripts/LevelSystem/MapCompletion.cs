using System;
using UnityEngine;

namespace TowerDefense
{
    public class MapCompletion : SingletonBase<MapCompletion>
    {
        public const string Filename = "completion.dat";

        [Serializable]
        private class EpisodeScore
        {
            public Episode episode;
            public int score;
        }

        [SerializeField] private EpisodeScore[] completionData;

        public static void SaveEpisodeResult(int levelScore)
        {
            if (Instance)
            {
                Instance.SaveResult(LevelSequenceController.Instance.CurrentEpisode, levelScore);
            }
            else
            {
                Debug.Log($"Episode completed with score {levelScore}");
            }
        }

        private new void Awake()
        {
            base.Awake();

            Saver<EpisodeScore[]>.TryLoad(Filename, ref completionData);

        }

        private void SaveResult(Episode currentEpisode, int levelScore)
        {
            foreach (var item in completionData)
            {
                if (item.episode == currentEpisode)
                {
                    if (levelScore > item.score) 
                    { 
                        item.score = levelScore;
                        Saver<EpisodeScore[]>.Save(Filename, completionData);
                    }
                }
            }
        }
        
        public int TotalScore
        {
            get
            {
                int totalScore = 0;
                foreach (var episodeScore in completionData)
                {
                    totalScore += episodeScore.score;
                }
                return totalScore;
            }
        }

        public int GetEpisodeScore(Episode m_Episode)
        {
            foreach (var data in completionData)
            {
                if (data.episode == m_Episode)
                {
                    return data.score;
                }
            }

            return 0;
        }
    }
}