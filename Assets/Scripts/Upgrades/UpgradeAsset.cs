using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu]
    public sealed class UpgradeAsset : ScriptableObject
    {
        [Header("Look")]
        public Sprite sprite;

        [Header("Cost")]
        public int[] costByLevel = { 3 };
    }
}
