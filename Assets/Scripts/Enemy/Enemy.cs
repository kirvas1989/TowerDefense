using UnityEngine;
using UnityEditor;
using System;

namespace TowerDefense
{
    [RequireComponent(typeof(TDPatrolController))]
    [RequireComponent(typeof(Destructible))]
    public class Enemy : MonoBehaviour
    {
        public enum ArmorType 
        { 
            Base = 0,
            Magic = 1,
            Elemental = 3
        }

        public static Func<int, Projectile.DamageType, int, int>[] ArmorDamageFunctions =
        {
            (int damage, Projectile.DamageType type, int armor) =>
            {
                // ArmorType.Base
                switch (type)
                {
                    case Projectile.DamageType.Magic: return damage; 
                    case Projectile.DamageType.Elemental: return damage / 2; 
                    default: return Mathf.Max(damage - armor, 1); 
                }
            },

            (int damage, Projectile.DamageType type, int armor) =>
            {        
                // ArmorType.Magic
                switch(type)
                {
                    case Projectile.DamageType.Base: return damage;
                    case Projectile.DamageType.Elemental: return damage / 2;
                    default: return Mathf.Max(damage - armor, 1); 
                }
            },

            (int damage, Projectile.DamageType type, int armor) =>
            {
                // ArmorType.Elemental
                switch(type)
                {
                    case Projectile.DamageType.Base: return damage / 2;
                    case Projectile.DamageType.Magic: return damage / 2;
                    default: return Mathf.Max(damage - armor, 1);
                }
            },
        };

        [SerializeField] private ArmorType m_ArmorType;
        [SerializeField] private int m_Damage = 1;
        [SerializeField] private int m_Gold = 1;
        [SerializeField] private int m_Armor = 0;

        private Destructible m_Destructible;
        public event Action OnEnd;
        float m_Timer = 1;
        private bool isDamaged = false;
        public bool IsDamaged => isDamaged;      

        private void Awake()
        {
            m_Destructible = GetComponent<Destructible>();
        }

        private void Update()
        {
            if (isDamaged)
            {
                if (m_Timer > 0)
                {
                    m_Timer -= Time.deltaTime;
                }
                else
                {
                    isDamaged = false;
                    m_Timer = 1;
                }
            }                 
        }

        private void OnDestroy()
        {
            OnEnd?.Invoke();
        }

        public void Use(EnemyAsset asset)
        {
            var sr = transform.Find("VisualModel").GetComponent<SpriteRenderer>();
            sr.color = asset.color;
            sr.transform.localScale = new Vector3(asset.spiteScale.x, asset.spiteScale.y, 1);
            sr.GetComponent<Animator>().runtimeAnimatorController = asset.animations;
            GetComponent<SpaceShip>().Use(asset);
            var col = GetComponentInChildren<CircleCollider2D>();
            col.radius = asset.colliderRadius;
            col.offset = asset.colliderOffset;
            GetComponentInChildren<CircleCollider2D>().radius = asset.colliderRadius;
            m_Damage = asset.damage;
            m_Gold = asset.gold;
            m_Armor = asset.armor;

            if (asset.m_ArmorType == EnemyAsset.ArmorType.Base)
                m_ArmorType = ArmorType.Base;

            if (asset.m_ArmorType == EnemyAsset.ArmorType.Magic)
                m_ArmorType = ArmorType.Magic; 
            
            if (asset.m_ArmorType == EnemyAsset.ArmorType.Elemental)
                m_ArmorType = ArmorType.Elemental; 
        }

        public void TakeDamage(int damage, Projectile.DamageType damageType)
        {
            m_Destructible.ApplyDamage(ArmorDamageFunctions[(int) m_ArmorType](damage, damageType, m_Armor));
            isDamaged = true;
        }

        public void DamagePlayer()
        {
            TDPlayer.Instance.ReduceLives(m_Damage);
        }

        public void GivePlayerGold()
        {
            TDPlayer.Instance.ChangeGold(m_Gold);
        }
    }

#if UNITY_EDITOR

    [CustomEditor(typeof(Enemy))]
    public class EnemyInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EnemyAsset asset = EditorGUILayout.ObjectField(null, typeof(EnemyAsset), false) as EnemyAsset;

            if (asset)
            {
                (target as Enemy).Use(asset);   
            }
        }
    }

#endif
} 