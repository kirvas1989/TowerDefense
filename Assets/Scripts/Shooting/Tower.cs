 using UnityEngine;

namespace TowerDefense
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private float m_Radius;

        private Turret[] turrets;
        private Destructible target = null;

        private void Start()
        {
            turrets = GetComponentsInChildren<Turret>();
        }

        private void Update()
        {
            if (target) //0
            {
                //Vector2 targetVector = target.transform.position - transform.position;
                //var targetVelocity = (Vector2)target.Velocity; 

                //if (targetVector.magnitude <= m_Radius)
                //{
                //    if (turrets != null && turrets.Length > 0)
                //    {
                //        foreach (var turret in turrets)
                //        {
                //            Vector2 leadPosition = (Vector2)target.transform.position + 
                //                                    targetVelocity * targetVector.magnitude / turret.ProjectileVelocity();

                //            turret.transform.up = leadPosition - (Vector2)turret.transform.position;
                //            turret.Fire();

                //            Debug.DrawRay(transform.position, leadPosition, Color.yellow);  
                //        }
                //    }
                //}

                //Vector2 targetVector = target.transform.position - transform.position;
                //var targetVelocity = (Vector2)target.Velocity;

                if (turrets != null && turrets.Length > 0) //1
                {
                    foreach (var turret in turrets) //2
                    {
                        Vector2 targetVector = target.transform.position - turret.transform.position;
                        var targetVelocity = (Vector2)target.Velocity;
                        if (targetVector.magnitude <= m_Radius) //3
                        {
                            Vector2 leadPosition = (Vector2)target.transform.position + 
                                                   targetVelocity * targetVector.magnitude / turret.ProjectileVelocity();
                            turret.transform.up = leadPosition - (Vector2)turret.transform.position;
                            turret.Fire();

                            Debug.DrawRay(transform.position, leadPosition, Color.yellow);
                        }
                        else //3
                        {
                            target = null;
                        }
                    }
                }
            }
            else //0
            {
                var enter = Physics2D.OverlapCircle(transform.position, m_Radius);

                if (enter)
                {
                    target = enter.transform.root.GetComponent<Destructible>();
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, m_Radius);
        }

        //public void Use(TowerAsset asset)
        //{
        //    GetComponentInChildren<SpriteRenderer>().sprite = asset.Sprite;
        //    turrets = GetComponentsInChildren<Turret>();
        //    //foreach (var turret in turrets)
        //    //{
        //    //    turret.AssignLoadOut(asset.TurretPropertiesAsset);
        //    //}
        //    var buildSite = GetComponentInChildren<BuildSite>();
        //    buildSite.SetBuildableTowers(asset.UpgradesTo);
        //}

        private int cost;
        public int Cost => cost;
        public void SetCost(int value)
        {
            cost = value;
        }
    }
}
