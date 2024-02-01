using UnityEngine;
using System;

namespace TowerDefense
{
    public class MapLevel : MonoBehaviour
    {
        [SerializeField] private Episode m_Episode;
        [SerializeField] private RectTransform resultPanel;
        [SerializeField] private GameObject[] progressIcon;

        public event Action<bool> OnLevelComplete;
        
        public bool IsComplete
        {
            get
            {
                return gameObject.activeSelf && progressIcon[0].gameObject.activeSelf;
            }
        } 

        public void LoadLevel()
        {
            LevelSequenceController.Instance.StartEpisode(m_Episode);
        }

        public int Initialize()
        {
            var score = MapCompletion.Instance.GetEpisodeScore(m_Episode);
            
            resultPanel.gameObject.SetActive(true);

            for (int i = 0; i < score; i++)
            {
                progressIcon[i].gameObject.SetActive(true);
            }

            if (score > 0) 
                OnLevelComplete?.Invoke(true);

            return score; 
        }
    }
}