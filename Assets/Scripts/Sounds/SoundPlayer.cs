using System;
using UnityEngine;

namespace TowerDefense
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : SingletonBase<SoundPlayer>
    {
        [SerializeField] private SoundsAsset m_Sounds;
        [SerializeField] private AudioClip m_BGM;

        private AudioSource m_AudioSource;

        private new void Awake()
        {
            base.Awake();
            m_AudioSource = GetComponent<AudioSource>();
            Instance.m_AudioSource.clip = m_BGM;
            Instance.m_AudioSource.Play();
        }

        public void Play(Sound sound)
        {
            m_AudioSource.PlayOneShot(m_Sounds[sound]);
        }

        public void PlayRandom(AudioClip m_AudioClip)
        {
            m_AudioSource.PlayOneShot(m_AudioClip);
        }
    }
}
