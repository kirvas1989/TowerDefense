using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

namespace TowerDefense
{
    public class Abilities : SingletonBase<Abilities>
    {
        #region FireRangeAbility

        [SerializeField] private FireRangeAbility m_FireRangeAbility;
        //[SerializeField] private Image m_TargetCircle;
        
        public void UseFireRangeAbility() => m_FireRangeAbility.Use();

        [Serializable]
        public class FireRangeAbility
        {
            [SerializeField] private UpgradeAsset m_FireRangeUpgrade;
            public UpgradeAsset FireRangeUpgrade => m_FireRangeUpgrade;
            [SerializeField] private int m_Cost = 10;
            public int Cost => m_Cost;
            [SerializeField] private int m_Damage;
            [SerializeField] private float m_Radius;
            public float Radius => m_Radius;    
            //[SerializeField] private Color m_TargetCircleColor;
            //public Color TargetCircleColor => m_TargetCircleColor;
            [SerializeField] private Projectile.DamageType m_DamageType;
            [SerializeField] private Sound m_Sound;

            public static event Action<bool> OnFireRange;

            public void Use()
            {   
                OnFireRange?.Invoke(true);
                Fire();
            }

            private void Fire()
            {
                ClickProtection.Instance.Activate((Vector2 vector) =>
                {
                    Vector3 position = vector;
                    position.z = -Camera.main.transform.position.z;
                    position = Camera.main.ScreenToWorldPoint(position);
                    OnFireRange?.Invoke(false);

                    var level = Upgrades.GetUpgradeLevel(m_FireRangeUpgrade);
                    float multiplier = 0.25f;
                    foreach (var collider in Physics2D.OverlapCircleAll(position, m_Radius + multiplier * level))
                    {
                        if (collider.transform.parent.TryGetComponent<Enemy>(out var enemy))
                        {
                            enemy.TakeDamage(m_Damage * level, m_DamageType);
                        }
                    }

                    TDPlayer.Instance.ChangeEnergy(-m_Cost);
                    OnFireRange?.Invoke(false);
                    m_Sound.Play(); 
                });
            }
        }

        #endregion

        #region StopTimeAbility

        //[SerializeField] private Button m_StopTimeButton;
        [SerializeField] private StopTimeAbility m_StopTimeAbility;
        public void UseStopTimeAbility() => m_StopTimeAbility.Use();

        [Serializable]
        public class StopTimeAbility
        {
            [SerializeField] private UpgradeAsset m_StopTimeUpgrade;
            public UpgradeAsset StopTimeUpgrade => m_StopTimeUpgrade;
            [SerializeField] private int m_Cost = 10;
            public int Cost => m_Cost;
            
            [SerializeField] private float m_Duration = 5;
            public float Duration => m_Duration;

            [SerializeField] private float m_Cooldown = 10;
            [SerializeField] private Sound m_Sound;

            public float Cooldown
            {
                get
                {
                    var level = Upgrades.GetUpgradeLevel(m_StopTimeUpgrade);
                    int multiplier = 5;
                    float cooldown = m_Cooldown + level * multiplier;
                    return cooldown;
                }
            }

            public static event Action<bool> OnStopTime;

            public void Use()
            {
                m_Sound.Play();
                TDPlayer.Instance.ChangeEnergy(-m_Cost);

                void Slow(Enemy enemy) 
                { 
                    enemy.GetComponent<SpaceShip>().HalfMaxLinearVelocity();
                }

                // Slow.
                foreach (var ship in FindObjectsOfType<SpaceShip>())
                    ship.HalfMaxLinearVelocity();
                EnemyWaveManager.OnEnemySpawn += Slow;
                OnStopTime?.Invoke(true);

                // Restore.
                var level = Upgrades.GetUpgradeLevel(m_StopTimeUpgrade);
                Instance.StartCoroutine(Restore());
                IEnumerator Restore()
                {
                    yield return new WaitForSeconds(m_Duration * level);
                    foreach (var ship in FindObjectsOfType<SpaceShip>())
                        ship.RestoreMaxLinearVelocity();
                    EnemyWaveManager.OnEnemySpawn -= Slow;
                }

                // Mentor's unused method from video.

                //Instance.StartCoroutine(StopTimeAbilityButton());
                //IEnumerator StopTimeAbilityButton()
                //{
                //    //Instance.m_StopTimeButton.interactable = false;
                //    OnStopTime?.Invoke(true);
                //    yield return new WaitForSeconds(m_Cooldown);
                //    //Instance.m_StopTimeButton.interactable = true;
                //    OnStopTime?.Invoke(false);
                //}
            }
        }

        #endregion
    }
}
