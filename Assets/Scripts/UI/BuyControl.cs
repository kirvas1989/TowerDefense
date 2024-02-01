using UnityEngine;
using System.Collections.Generic;

namespace TowerDefense
{
    public class BuyControl : MonoBehaviour
    {
        [SerializeField] private TowerBuyControl m_TowerBuyPrefab;
        [SerializeField] private TowerSellControl towerSell;

        private List<TowerBuyControl> m_ActiveControl;
        private RectTransform m_RectTransform;

        private Vector3 direction;

        private void Awake()
        {
            m_RectTransform = GetComponent<RectTransform>();
            BuildSite.OnClickEvent += MoveToBuildSite;
            gameObject.SetActive(false);            
        }

        private void OnDestroy()
        {
            BuildSite.OnClickEvent -= MoveToBuildSite;
        }

        private void MoveToBuildSite(Transform buildSite)
        {
            if (buildSite)
            {
                var position = Camera.main.WorldToScreenPoint(buildSite.position);
                m_RectTransform.anchoredPosition = position;
                m_ActiveControl = new List<TowerBuyControl>();

                var bs = buildSite.GetComponent<BuildSite>();
                if (bs.IsEmpty == true)
                {
                    towerSell.gameObject.SetActive(false);

                    foreach (var asset in bs.BuildableTowers)
                        InitTowerAsset(asset);

                    if (m_ActiveControl.Count == 2)
                    {
                        direction = Vector3.left;
                    }
                    else if (m_ActiveControl.Count > 2)
                    {
                        direction = Vector3.up;
                    }    

                    InitTowerBuyUI(buildSite);
                }
                else
                {
                    Tower[] towers = FindObjectsOfType<Tower>();
                    if (towers.Length > 0)
                    {
                        foreach (var tower in towers)
                        {
                            if (tower.transform.position == buildSite.position)
                            {
                                var towerBS = tower.GetComponentInChildren<BuildSite>();
                                if (towerBS != null)
                                {
                                    foreach (var asset in towerBS.BuildableTowers)
                                        InitTowerAsset(asset);

                                    direction = Vector3.up;

                                    InitTowerBuyUI(buildSite);
                                }
                            }
                        }
                    }

                    towerSell.SetBuildSite(buildSite);
                    towerSell.gameObject.SetActive(true);
                }
            }
            else
            {
                TowerBuyControl[] TowerBuyControls = FindObjectsOfType<TowerBuyControl>();
                foreach (var buyControl in TowerBuyControls)
                {
                    Destroy(buyControl.gameObject);
                }

                if (m_ActiveControl != null) m_ActiveControl.Clear();
                towerSell.gameObject.SetActive(false);
            }
        }

        private void InitTowerBuyUI(Transform buildSite)
        {
            if (m_ActiveControl.Count > 0)
            {
                gameObject.SetActive(true);
                var angle = 360 / m_ActiveControl.Count;

                for (int i = 0; i < m_ActiveControl.Count; i++)
                {
                    var offset = Quaternion.AngleAxis(angle * i, Vector3.forward) * (direction * 85);
                    m_ActiveControl[i].transform.position += offset;
                }
            }

            foreach (var tbc in GetComponentsInChildren<TowerBuyControl>())
            {
                tbc.SetBuildSite(buildSite);
            }
        }

        private void InitTowerAsset(TowerAsset asset)
        {
            if (asset.IsAvaliable())
            {
                var newControl = Instantiate(m_TowerBuyPrefab, transform);
                m_ActiveControl.Add(newControl);
                newControl.SetTowerAsset(asset);
            }
        }
    }
}