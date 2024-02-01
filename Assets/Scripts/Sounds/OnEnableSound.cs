using UnityEngine;

namespace TowerDefense
{
    public class OnEnableSound : MonoBehaviour
    {
        [SerializeField] private Sound m_Sound;

        private void OnEnable()
        {
            m_Sound.Play();
        }
    }
}