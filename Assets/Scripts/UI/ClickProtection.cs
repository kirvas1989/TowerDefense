using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TowerDefense
{
    public class ClickProtection : SingletonBase<ClickProtection>, IPointerDownHandler
    {
        [SerializeField] private Image m_Blocker;

        private Action<Vector2> m_OnClickAction;

        public void Activate(Action<Vector2> mouseAction)
        {
            m_Blocker.enabled = true;
            m_OnClickAction = mouseAction;  
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            m_Blocker.enabled = false;
            m_OnClickAction(eventData.pressPosition);
            m_OnClickAction = null;
        }
    }
}