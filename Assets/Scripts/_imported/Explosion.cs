using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// A����-���������� ���������� ������.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class Explosion : AnimateSpriteFrames
    {
        //[SerializeField] protected Explosion m_ExplosionPrefab;

        [Header("Audio")]
        [SerializeField] protected AudioSource m_AudioSource;
        [SerializeField] protected AudioClip m_Clip;
        [SerializeField] protected float volume;

        public virtual void Explode()
        {
            StartAnimation(true);

            if (m_AudioSource != null && m_AudioSource.clip != null)
                m_AudioSource?.PlayOneShot(m_Clip, volume);

            Destroy(gameObject, m_AnimationTime);
        }
    }
}
