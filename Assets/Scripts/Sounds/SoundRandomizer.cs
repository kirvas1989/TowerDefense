using UnityEngine;

namespace TowerDefense
{
    public class SoundRandomizer : MonoBehaviour
    {
        [SerializeField] private AudioClip[] m_AudioClips;   
        private AudioClip m_AudioClip;

        public void Play() 
        {
            int randomIndex = Random.Range(0, m_AudioClips.Length);
            m_AudioClip = m_AudioClips[randomIndex];
            SoundPlayer.Instance.PlayRandom(m_AudioClip);
        }
    }
}