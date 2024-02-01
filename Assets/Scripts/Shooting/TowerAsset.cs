﻿using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu]
    public class TowerAsset : ScriptableObject
    {
        public int GoldCost = 15;       
        public Sprite Sprite;
        public Color Color = Color.white;
        public Sprite GUISprite;

        //
        [SerializeField] private UpgradeAsset RequiredUpgrade;
        [SerializeField] private int RequiredUpgradeLevel;
        public TowerAsset[] UpgradesTo;
        public bool IsAvaliable() => !RequiredUpgrade ||
            RequiredUpgradeLevel <= Upgrades.GetUpgradeLevel(RequiredUpgrade);
        //

        [SerializeField] public TurretProperties TurretPropertiesAsset;
    }
}