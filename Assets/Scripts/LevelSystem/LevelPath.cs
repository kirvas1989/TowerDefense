using UnityEngine;

namespace TowerDefense
{
    public class LevelPath : MonoBehaviour
    {
        [SerializeField] private MapLevel m_level;
        [SerializeField] private GameObject m_visualModel;

        private void Awake()
        {
            m_visualModel.SetActive(false);
        }

        private void Start()
        {
            m_level.OnLevelComplete += OpenPath;          
        }

        private void OpenPath(bool active)
        {
            m_visualModel.SetActive(active);
            m_level.OnLevelComplete -= OpenPath;
        }
    }
}