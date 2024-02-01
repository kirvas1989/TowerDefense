using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TowerDefense
{
    [CreateAssetMenu]
    public class SoundsAsset : ScriptableObject
    {
        [SerializeField] private AudioClip[] m_Sounds;
        public AudioClip this[Sound sound] => m_Sounds[(int)sound];


#if UNITY_EDITOR

        [CustomEditor(typeof(SoundsAsset))]
        public class SoundsInspector : Editor
        {
            private static readonly int soundCount = Enum.GetValues(typeof(Sound)).Length;
            private new SoundsAsset target => base.target as SoundsAsset;
            
            public override void OnInspectorGUI()
            {
                if(target.m_Sounds.Length < soundCount)
                {
                    Array.Resize(ref target.m_Sounds, soundCount);  
                }

                for (int i = 0; i < target.m_Sounds.Length; i++)
                {
                    target.m_Sounds[i] = EditorGUILayout.ObjectField($"{(Sound)i}: ", target.m_Sounds[i], typeof(AudioClip), false) as AudioClip;
                }

                EditorUtility.SetDirty(target);
            }
        }

#endif
    }
}
