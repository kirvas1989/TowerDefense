using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu]
    public sealed class EnemyAsset : ScriptableObject 
    {
        public enum ArmorType
        {
            Base,
            Magic,
            Elemental
        }

        [Header("Look")]
        public RuntimeAnimatorController animations;
        public Color color = Color.white;
        public Vector2 spiteScale = new Vector2(2, 2);
        public Vector2 colliderOffset = new Vector2(0, 0.1f);
        public float colliderRadius = 0.1f;
             
        [Header("Stats")]
        [SerializeField] public ArmorType m_ArmorType;
        public float moveSpeed = 1;
        public int hp = 1;
        public int armor = 0;
        public int score = 1;     
        public int damage = 1;
        public int gold = 1;
    }
}
