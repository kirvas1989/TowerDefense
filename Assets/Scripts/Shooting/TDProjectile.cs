using UnityEngine;

namespace TowerDefense
{
    public class TDProjectile : Projectile
    {
        //private float m_FreezeRate;
        //private bool m_Freeze = false;
        //private bool m_DoNotDestroyOnHit = false;

        //protected override void OnHit(RaycastHit2D hit)
        //{
        //    var enemy = hit.collider.transform.root.GetComponent<Enemy>();

        //    if (enemy != null)
        //    {
        //        if (m_Freeze == true)
        //        {
        //            var ship = enemy.GetComponent<SpaceShip>();
        //            var sr = enemy.GetComponentInChildren<SpriteRenderer>();

        //            if (ship && sr)
        //            {
        //                ship.SlowDown(m_FreezeRate);
        //                sr.color = Color.cyan;
        //            }
        //        }

        //        enemy.TakeDamage(m_Damage);

        //        if (m_DoNotDestroyOnHit == false)
        //        {
        //            OnProjectileLifeEnd(hit.collider, hit.point);
        //        }
        //    }
        //}

        //public void Use(ProjectileAsset asset)
        //{
        //    var sr = transform.Find("VisualModel").GetComponent<SpriteRenderer>();
        //    sr.sprite = asset.Sprite;
        //    m_Velocity = asset.Velocity;
        //    m_Lifetime = asset.Lifetime;
        //    m_Damage = asset.Damage;
        //    m_ImpactEffectPrefab = asset.ImpactEffectPrefab;
        //    m_bombPrefab = asset.BombPrefab;
        //    m_AudioSource = asset.AudioSource;
        //    m_FreezeRate = asset.FrozenSpeed;
        //    m_Freeze = asset.Freeze;
        //    m_DoNotDestroyOnHit = asset.DoNotDestroyOnHit;
        //}
    }
}