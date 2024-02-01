using UnityEngine;
using UnityEngine.UI;
using static TowerDefense.Abilities;

namespace TowerDefense
{
    public class StopTimeAbilityUI : MonoBehaviour
    {
        [SerializeField] private StopTimeAbility m_StopTimeAbility;
        [SerializeField] private Button m_StopTimeButton;
        [SerializeField] private Image m_LockImage;
        [SerializeField] private Text m_EnergyCostText;
       
        private float timer;
        private bool isActivated;

        private void Start()
        {
            m_LockImage.gameObject.SetActive(false);
            m_EnergyCostText.text = m_StopTimeAbility.Cost.ToString();
            StopTimeAbility.OnStopTime += OnStopTimeEvent;   
        }

        private void OnDestroy()
        {
            StopTimeAbility.OnStopTime -= OnStopTimeEvent;
        }

        private void Update()
        {
            if (m_StopTimeAbility.Cost > TDPlayer.Instance.CurrentEnergy)           
                m_EnergyCostText.color = Color.red;
            else
                m_EnergyCostText.color = Color.white;

            if (m_StopTimeAbility.Cost > TDPlayer.Instance.CurrentEnergy || isActivated)          
                m_StopTimeButton.interactable = false;            
            else if (m_StopTimeAbility.Cost <= TDPlayer.Instance.CurrentEnergy && isActivated == false)
                m_StopTimeButton.interactable = true;

            if (isActivated)
            {
                timer -= Time.deltaTime;            
                m_LockImage.fillAmount = timer / m_StopTimeAbility.Cooldown;

                if (m_LockImage.fillAmount == 0) OnStopTimeEvent(false);
            }
        }

        private void OnStopTimeEvent(bool active)
        {
            isActivated = active;
            //m_StopTimeButton.interactable = !active;
            m_LockImage.gameObject.SetActive(active);
            m_LockImage.fillAmount = 1;
            timer = m_StopTimeAbility.Cooldown;
        }
    }
}
