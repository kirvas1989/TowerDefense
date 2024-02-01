using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace TowerDefense
{
    public class BuildSite : MonoBehaviour, IPointerDownHandler
    {
        public TowerAsset[] BuildableTowers;

        public static event Action<Transform> OnClickEvent;
        
        private bool isEmpty = true;
        public bool IsEmpty => isEmpty;
         
        public static void HideControls()
        {
            OnClickEvent(null);
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            OnClickEvent(transform.root);
        }
        
        public void AllowToBuild(bool value)
        {
            isEmpty = value;
        }

        public void SetBuildableTowers(TowerAsset[] towers)
        {
            // Возможно здесь должен быть код на продажу башни.
            if (towers == null || towers.Length == 0)
            {
                Destroy(transform.parent.gameObject);                
            }
            else
            {
                BuildableTowers = towers;
            }
        }
    } 
}