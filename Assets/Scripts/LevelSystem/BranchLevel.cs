using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    [RequireComponent(typeof(MapLevel))]
    public class BranchLevel : MonoBehaviour
    {
        [SerializeField] private MapLevel rootLevel;
        [SerializeField] private Text pointText;
        [SerializeField] private GameObject lockPanel;
        [SerializeField] private int needPoints = 3;

        public bool RootIsActive { get { return rootLevel.IsComplete; } }

        /// <summary>
        /// ѕопытка активации ответвленного уровн€.
        /// јктиваци€ требует наличи€ очков и выполнени€ прошлого уровн€.
        /// </summary>
        public void TryActivate()
        {
            gameObject.SetActive(rootLevel.IsComplete);

            if (needPoints > MapCompletion.Instance.TotalScore)
            {
                pointText.text = "Need: " + needPoints.ToString();               
            }
            else
            {
                lockPanel.SetActive(false);
                GetComponent<MapLevel>().Initialize();
            }
        }
    }
}